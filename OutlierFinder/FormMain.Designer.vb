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
        CType(dataGrid, ComponentModel.ISupportInitialize).BeginInit()
        CType(picMain, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' dataGrid
        ' 
        dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dataGrid.Location = New Point(443, 38)
        dataGrid.Name = "dataGrid"
        dataGrid.Size = New Size(334, 312)
        dataGrid.TabIndex = 0
        ' 
        ' picMain
        ' 
        picMain.Location = New Point(24, 38)
        picMain.Name = "picMain"
        picMain.Size = New Size(392, 372)
        picMain.TabIndex = 1
        picMain.TabStop = False
        ' 
        ' btnFindBFC
        ' 
        btnFindBFC.Location = New Point(443, 371)
        btnFindBFC.Name = "btnFindBFC"
        btnFindBFC.Size = New Size(205, 39)
        btnFindBFC.TabIndex = 2
        btnFindBFC.Text = "Find && Draw"
        btnFindBFC.UseVisualStyleBackColor = True
        ' 
        ' btnClearPoints
        ' 
        btnClearPoints.Location = New Point(683, 371)
        btnClearPoints.Name = "btnClearPoints"
        btnClearPoints.Size = New Size(94, 39)
        btnClearPoints.TabIndex = 2
        btnClearPoints.Text = "Clear Points"
        btnClearPoints.UseVisualStyleBackColor = True
        ' 
        ' FormMain
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(btnClearPoints)
        Controls.Add(btnFindBFC)
        Controls.Add(picMain)
        Controls.Add(dataGrid)
        Name = "FormMain"
        Text = "Best Fit Circle Finder"
        CType(dataGrid, ComponentModel.ISupportInitialize).EndInit()
        CType(picMain, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents dataGrid As DataGridView
    Friend WithEvents picMain As PictureBox
    Friend WithEvents btnFindBFC As Button
    Friend WithEvents btnClearPoints As Button

End Class
