Imports System.Globalization
Imports System.Configuration
Imports System.Collections.Specialized
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Net.Mail

Module LoanBalancing

    Public dsLoans As DataSet
    Public dsHoldings As DataSet

    Private Function ExecuteIT() As String
        Dim MySQL, strConn, sHTML, bHTML, sUsers As String
        Dim MyConn As FirebirdSql.Data.FirebirdClient.FbConnection
        Dim Cmd As FirebirdSql.Data.FirebirdClient.FbCommand
        Dim Adaptor As FirebirdSql.Data.FirebirdClient.FbDataAdapter
        Dim dr, dr1 As DataRow
        sUsers = ConfigurationManager.AppSettings("EmailList")



        strConn = ConfigurationManager.ConnectionStrings("FBConnectionString").ConnectionString
        MyConn = New FirebirdSql.Data.FirebirdClient.FbConnection(strConn)
        MyConn.Open()

        MySQL = "Select  l.loanid, l.business_name, l.fixed_rate, l.MAXLOANAMOUNT
                	From Loans l
    	            Where  l.LOANSTATUS in (2, 7)
                        and l.isactive = 0 
                        order by  l.loanid"

        dsLoans = New DataSet
        Adaptor = New FirebirdSql.Data.FirebirdClient.FbDataAdapter(MySQL, MyConn)

        Adaptor.Fill(dsLoans)

        MyConn.Close()

        Dim errorcount As Integer = Nothing


        Dim iLoanCounter As Integer = dsLoans.Tables(0).Rows.Count

        For i = 0 To iLoanCounter - 1
            dr = dsLoans.Tables(0).Rows(i)
            Dim loanid As Integer = dr("LoanID")

            Dim loanname As String = dr("Business_Name")
            Dim fixedrate As Integer = dr("Fixed_Rate")
            Dim maxloanamount As Integer = dr("maxloanamount")
            Dim smryLoanID, smryloanamount, smrytotal As Integer
            smryLoanID = loanid
            smryloanamount = maxloanamount

            MyConn.Open()

            MySQL = "Select  h.loan_holdings_id, h.rate, h.num_units, sum(b.num_units) as sumbalance
                    From loan_holdings h, lh_balances b
                    Where  h.loanid = " & loanid &
                      "  and h.loan_holdings_id = b.lh_id
                        and h.isactive = 0 
                        group by h.loan_holdings_id,  h.rate,  h.num_units
                      

                    union all
                    Select  h.loan_holdings_id, h.rate, h.num_units, sum(b.num_units) as sumbalance
                    From loan_holdings h, lh_balances_suspense b
                    Where  h.loanid = " & loanid &
                      "  and h.loan_holdings_id = b.lh_id
                        and h.isactive = 0 
                        group by h.loan_holdings_id,  h.rate,  h.num_units"



            dsHoldings = New DataSet
            Adaptor = New FirebirdSql.Data.FirebirdClient.FbDataAdapter(MySQL, MyConn)

            Adaptor.Fill(dsHoldings)

            MyConn.Close()

            Dim iHoldingCounter As Integer = dsHoldings.Tables(0).Rows.Count
            Dim lhbalance As Integer = 0

            For j = 0 To iHoldingCounter - 1
                dr1 = dsHoldings.Tables(0).Rows(j)
                Dim LHID As Integer = dr1("Loan_holdings_ID")
                Dim rate As Integer = dr1("rate")
                Dim numunits As Integer = dr1("num_units")
                Dim sumbalance As Integer = dr1("sumbalance")
                lhbalance += sumbalance



                'Dim row As String() = New String() {loanid, loanname, fixedrate, maxloanamount, LHID, rate, numunits, sumbalance}
                'DataGridView1.Rows.Add(row)


            Next j

            smrytotal = lhbalance
            Dim Accepted As Integer = 0
            Dim Difference As Integer = smryloanamount - smrytotal
            'read the entries in the config file to establish if there is still debris on the database for this loan 
            Try
                Dim llist As String = "l" & loanid
                Dim laccepted As String = ConfigurationManager.AppSettings(llist)
                If laccepted IsNot Nothing Then
                    Accepted = laccepted
                End If
            Catch ex As Exception

            End Try

            Dim Balance As Integer = Difference - Accepted
            errorcount = errorcount + Balance

            'Dim row2 As String() = New String() {smryLoanID, smryloanamount, smrytotal, Difference}
            'DataGridView2.Rows.Add(row2)

            bHTML &= "<tr><td>" & smryLoanID & "</td>"
            bHTML &= "<td>" & smryloanamount & "</td>"
            bHTML &= "<td>" & smrytotal & "</td>"
            bHTML &= "<td>" & Difference & "</td>"
            bHTML &= "<td>" & Accepted & "</td>"
            bHTML &= "<td>" & Balance & "</td></tr>"
            bHTML &= vbNewLine


        Next i


        'msgMessage.Text = "Extract Completed"



        If errorcount = 0 Then
            sHTML = "<html><body><head>
                <style>
                table {
                    font-family: arial, sans-serif;
                    border-collapse: collapse;
                    width: 100%;
                }

                td, th {
                    border: 1px solid #dddddd;
                    text-align: left;
                    padding: 8px;
                }

                tr:nth-child(even) {
                    background-color: #dddddd;
                }
                </style>
                </head>
                <table>
                  <tr>
                    <th style='font-size:30px' colspan=6>Loan Balances Report - no errors</th>
                   
                  </tr>
                  <tr>
                    <th>LoanID</th>
                    <th>LoanAmount</th>
                    <th>Total</th>
                    <th>Difference</th>
                    <th>Accepted</th>
                    <th>Balance</th>
                    
                  </tr>"
        Else
            sHTML = "<html><body><head>
                <style>
                table {
                    font-family: arial, sans-serif;
                    border-collapse: collapse;
                    width: 100%;
                }

                td, th {
                    border: 1px solid #dddddd;
                    text-align: left;
                    padding: 8px;
                }

                tr:nth-child(even) {
                    background-color: #dddddd;
                }
                </style>
                </head>
                <table>
                  <tr>
                    <th style='font-size:30px' colspan=6>Loan Balances Report - discrepancies encountered</th>
                   
                  </tr>
                  <tr>
                    <th>LoanID</th>
                    <th>LoanAmount</th>
                    <th>Total</th>
                    <th>Difference</th>
                    <th>Accepted</th>
                    <th>Balance</th>
                    
                  </tr>"
        End If

        sHTML &= bHTML
        sHTML &= "</table></body></html>"

        SendSimpleMail(sUsers, "Loan Balances Report", sHTML)

        ExecuteIT = sHTML
    End Function

    Sub Main()
        ExecuteIT()
    End Sub



    Public Sub SendSimpleMail(sEmail As String, sSubject As String, sBody As String)
        Dim sPW As String = ConfigurationManager.AppSettings("FinanceBalancingPW")
        Dim sUSR As String = ConfigurationManager.AppSettings("FinanceBalancingUSR")
        Dim MyMailMessage As New MailMessage() With {
            .From = New MailAddress(sUSR),
            .Subject = sSubject,
            .IsBodyHtml = True,
            .Body = "<table><tr><td>" + sBody + "</table></td></tr>"
        }
        MyMailMessage.To.Add(sEmail)

        Dim SMTPServer As New SmtpClient("smtp.office365.com") With {
            .Credentials = New System.Net.NetworkCredential(sUSR, sPW),
            .Port = 587,
            .EnableSsl = True
        }

        Try
            SMTPServer.Send(MyMailMessage)
        Catch ex As Exception
            SendErrorMessage(ex)
        End Try
        SMTPServer = Nothing
        MyMailMessage = Nothing
    End Sub

    Sub SendErrorMessage(ByVal ThisException As Exception)
        Dim errorPW As String = ConfigurationManager.AppSettings("ErrorPW")
        Dim errorUSR As String = ConfigurationManager.AppSettings("ErrorUSR")
        Dim mm As New MailMessage() With {
            .From = New MailAddress(errorUSR),
            .Subject = "An Error Has Occurred!",
            .IsBodyHtml = True,
            .Priority = MailPriority.High
        }
        mm.To.Add("web@investandfund.com")

        mm.Body =
            "<html>" & vbCrLf &
            "<body>" & vbCrLf &
            "<h1>An Error Has Occurred!</h1>" & vbCrLf &
            "<table cellpadding=""5"" cellspacing=""0"" border=""1"">" & vbCrLf &
            ItemFormat("Time of Error", DateTime.Now.ToString("dd/MM/yyyy HH:mm:sss"))

        Try
            mm.Body += ItemFormat("Exception Type", ThisException.GetType().ToString())
        Catch ex As Exception
            mm.Body += ItemFormat("Exception Type", "Could not get exception type")
        End Try

        Try
            mm.Body += ItemFormat("Message", ThisException.Message)
        Catch ex As Exception
            mm.Body += ItemFormat("Message", "Could not get message")
        End Try

        Try
            mm.Body += ItemFormat("File Name", "DailyReport.vb")
        Catch ex As Exception
            mm.Body += ItemFormat("File Name", "Could not get file name")
        End Try

        Try
            mm.Body += ItemFormat("Line Number", New StackTrace(ThisException, True).GetFrame(0).GetFileLineNumber)
        Catch ex As Exception
            mm.Body += ItemFormat("Line Number", "Could not get line number")
        End Try

        mm.Body +=
            "</table>" & vbCrLf &
            "</body>" & vbCrLf &
            "</html>"

        Dim smtp As New SmtpClient("smtp.office365.com") With {
            .Credentials = New System.Net.NetworkCredential(errorUSR, errorPW),
            .EnableSsl = True,
            .Port = 587
        }
        smtp.Send(mm)

    End Sub

    Public Function ItemFormat(ByVal Title As String, ByVal Message As String) As String
        Return "  <tr>" & vbCrLf &
                "  <tdtext-align: right;font-weight: bold"">" & Title & ":</td>" & vbCrLf &
                "  <td>" & Message & "</td>" & vbCrLf &
                "  </tr>" & vbCrLf
    End Function
End Module
