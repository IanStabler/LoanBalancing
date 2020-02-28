<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnGo = New System.Windows.Forms.Button()
        Me.msgMessage = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.LoanID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LoanName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FixedRate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MaxLoanAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LoanHoldingID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Rate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.NumUnits = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SumBalance = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.smryLoanID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.smryLoanAmt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.smryLoanTotal = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Difference = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnGo
        '
        Me.btnGo.Location = New System.Drawing.Point(12, 12)
        Me.btnGo.Name = "btnGo"
        Me.btnGo.Size = New System.Drawing.Size(158, 23)
        Me.btnGo.TabIndex = 0
        Me.btnGo.Text = "Perform Loan Balancing"
        Me.btnGo.UseVisualStyleBackColor = True
        '
        'msgMessage
        '
        Me.msgMessage.AutoSize = True
        Me.msgMessage.Location = New System.Drawing.Point(294, 17)
        Me.msgMessage.Name = "msgMessage"
        Me.msgMessage.Size = New System.Drawing.Size(53, 13)
        Me.msgMessage.TabIndex = 1
        Me.msgMessage.Text = "Message:"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.LoanID, Me.LoanName, Me.FixedRate, Me.MaxLoanAmount, Me.LoanHoldingID, Me.Rate, Me.NumUnits, Me.SumBalance})
        Me.DataGridView1.Location = New System.Drawing.Point(457, 51)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(1046, 813)
        Me.DataGridView1.TabIndex = 2
        '
        'DataGridView2
        '
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.smryLoanID, Me.smryLoanAmt, Me.smryLoanTotal, Me.Difference})
        Me.DataGridView2.Location = New System.Drawing.Point(13, 51)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(438, 813)
        Me.DataGridView2.TabIndex = 3
        '
        'LoanID
        '
        Me.LoanID.HeaderText = "LoanID"
        Me.LoanID.Name = "LoanID"
        Me.LoanID.Width = 70
        '
        'LoanName
        '
        Me.LoanName.HeaderText = "LoanName"
        Me.LoanName.Name = "LoanName"
        Me.LoanName.Width = 300
        '
        'FixedRate
        '
        Me.FixedRate.HeaderText = "FixedRate"
        Me.FixedRate.Name = "FixedRate"
        '
        'MaxLoanAmount
        '
        Me.MaxLoanAmount.HeaderText = "MaxLoanAmount"
        Me.MaxLoanAmount.Name = "MaxLoanAmount"
        '
        'LoanHoldingID
        '
        Me.LoanHoldingID.HeaderText = "LoanHoldingID"
        Me.LoanHoldingID.Name = "LoanHoldingID"
        '
        'Rate
        '
        Me.Rate.HeaderText = "Rate"
        Me.Rate.Name = "Rate"
        '
        'NumUnits
        '
        Me.NumUnits.HeaderText = "NumUnits"
        Me.NumUnits.Name = "NumUnits"
        '
        'SumBalance
        '
        Me.SumBalance.HeaderText = "SumBalance"
        Me.SumBalance.Name = "SumBalance"
        '
        'smryLoanID
        '
        Me.smryLoanID.HeaderText = "LoanID"
        Me.smryLoanID.Name = "smryLoanID"
        Me.smryLoanID.Width = 70
        '
        'smryLoanAmt
        '
        Me.smryLoanAmt.HeaderText = "LoanAmount"
        Me.smryLoanAmt.Name = "smryLoanAmt"
        '
        'smryLoanTotal
        '
        Me.smryLoanTotal.HeaderText = "LoanTotal"
        Me.smryLoanTotal.Name = "smryLoanTotal"
        '
        'Difference
        '
        Me.Difference.HeaderText = "Difference"
        Me.Difference.Name = "Difference"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1517, 876)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.msgMessage)
        Me.Controls.Add(Me.btnGo)
        Me.Name = "Form1"
        Me.Text = "Loan Balancing Report"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnGo As Button
    Friend WithEvents msgMessage As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents LoanID As DataGridViewTextBoxColumn
    Friend WithEvents LoanName As DataGridViewTextBoxColumn
    Friend WithEvents FixedRate As DataGridViewTextBoxColumn
    Friend WithEvents MaxLoanAmount As DataGridViewTextBoxColumn
    Friend WithEvents LoanHoldingID As DataGridViewTextBoxColumn
    Friend WithEvents Rate As DataGridViewTextBoxColumn
    Friend WithEvents NumUnits As DataGridViewTextBoxColumn
    Friend WithEvents SumBalance As DataGridViewTextBoxColumn
    Friend WithEvents smryLoanID As DataGridViewTextBoxColumn
    Friend WithEvents smryLoanAmt As DataGridViewTextBoxColumn
    Friend WithEvents smryLoanTotal As DataGridViewTextBoxColumn
    Friend WithEvents Difference As DataGridViewTextBoxColumn
End Class
