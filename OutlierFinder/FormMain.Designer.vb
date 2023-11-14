<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FormMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        dataGrid = New DataGridView()
        picMain = New PictureBox()
        btnFindBFC = New Button()
        btnClearPoints = New Button()
        btnFindTrendLine = New Button()
        CType(dataGrid, ComponentModel.ISupportInitialize).BeginInit()
        CType(picMain, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dataGrid
        ' 
        dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dataGrid.Location = New Point(537, 12)
        dataGrid.Name = "dataGrid"
        dataGrid.Size = New Size(351, 353)
        dataGrid.TabIndex = 0
        ' 
        ' picMain
        ' 
        picMain.Location = New Point(12, 12)
        picMain.Name = "picMain"
        picMain.Size = New Size(510, 398)
        picMain.TabIndex = 1
        picMain.TabStop = False
        ' 
        ' btnFindBFC
        ' 
        btnFindBFC.Location = New Point(24, 431)
        btnFindBFC.Name = "btnFindBFC"
        btnFindBFC.Size = New Size(233, 39)
        btnFindBFC.TabIndex = 2
        btnFindBFC.Text = "Find Best Fit Circle"
        btnFindBFC.UseVisualStyleBackColor = True
        ' 
        ' btnClearPoints
        ' 
        btnClearPoints.Location = New Point(590, 371)
        btnClearPoints.Name = "btnClearPoints"
        btnClearPoints.Size = New Size(244, 39)
        btnClearPoints.TabIndex = 2
        btnClearPoints.Text = "Clear Points"
        btnClearPoints.UseVisualStyleBackColor = True
        ' 
        ' btnFindTrendLine
        ' 
        btnFindTrendLine.Location = New Point(278, 431)
        btnFindTrendLine.Name = "btnFindTrendLine"
        btnFindTrendLine.Size = New Size(233, 39)
        btnFindTrendLine.TabIndex = 2
        btnFindTrendLine.Text = "Find Trend Line"
        btnFindTrendLine.UseVisualStyleBackColor = True
        ' 
        ' FormMain
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(900, 490)
        Controls.Add(btnClearPoints)
        Controls.Add(btnFindTrendLine)
        Controls.Add(btnFindBFC)
        Controls.Add(picMain)
        Controls.Add(dataGrid)
        Name = "FormMain"
        Text = "Outlier Finder"
        CType(dataGrid, ComponentModel.ISupportInitialize).EndInit()
        CType(picMain, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dataGrid As DataGridView
    Friend WithEvents picMain As PictureBox
    Friend WithEvents btnFindBFC As Button
    Friend WithEvents btnClearPoints As Button
    Friend WithEvents btnFindTrendLine As Button

End Class
