Imports System.Drawing
Imports System.Drawing.Text
Imports MathNet.Numerics



' Make sure to install the Math.NET Numerics library
' using NuGet Package Manager Console with the command:
' Install-Package MathNet.Numerics
Imports MathNet.Numerics.LinearAlgebra

Public Class FormMain
    Dim points As New List(Of PointS)
    Dim circle As New Circle

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Set up the PictureBox
        picMain.BorderStyle = BorderStyle.FixedSingle

        ' Set example points
        points.Add(New PointS With {.X = -50.25, .Y = 68.12})
        points.Add(New PointS With {.X = 115.3, .Y = 42.8})
        points.Add(New PointS With {.X = 80.9, .Y = -50.66})
        points.Add(New PointS With {.X = -60.52, .Y = -50.06})
        points.Add(New PointS With {.X = -36.33, .Y = -70.78})

        ' Update data grid view with points
        ' Set up the DataGridView
        dataGrid.ColumnCount = 3
        dataGrid.Columns(0).HeaderText = "X"
        dataGrid.Columns(1).HeaderText = "Y"
        dataGrid.Columns(2).HeaderText = "Data Analysis"

        For Each pt As PointS In points
            dataGrid.Rows.Add(pt.X.ToString, pt.Y.ToString, "")
        Next

        ' Use the Least Squares method to find the best-fit circle
        FindAndDrawCircle()
    End Sub
    Private Sub btnFindBFC_Click(sender As Object, e As EventArgs) Handles btnFindBFC.Click
        FindAndDrawCircle()
    End Sub
    Private Sub FindAndDrawCircle()
        ' Clear existing points
        points.Clear()

        ' Add new points from DataGridView
        For Each row As DataGridViewRow In dataGrid.Rows
            Dim x As Single
            Dim y As Single

            If row.Index >= dataGrid.Rows.Count - 1 Then
                Exit For
            End If

            Try
                ' Try to parse the values from the DataGridView cells
                If Single.TryParse(row.Cells(0).Value.ToString(), x) AndAlso Single.TryParse(row.Cells(1).Value.ToString(), y) Then
                    points.Add(New PointS With {.X = x, .Y = y})
                End If
            Catch ex As Exception
            End Try
        Next

        ' Use the Least Squares method to find the best-fit circle
        circle = CircleFit.FitCircle(points)

        ' Calculate the minimum distance of each point to circle
        For Each row As DataGridViewRow In dataGrid.Rows
            If row.Index >= dataGrid.Rows.Count - 1 Then
                Exit For
            End If

            Dim pt As PointS = points(row.Index)
            row.Cells(2).Value = CSng(Math.Abs(Math.Sqrt((pt.X - circle.CenterX) * (pt.X - circle.CenterX) +
                                           (pt.Y - circle.CenterY) * (pt.Y - circle.CenterY)) - circle.Radius))
        Next

        ' Draw the best fit circle
        DrawCircle(circle)
    End Sub

    Private Sub btnClearPoints_Click(sender As Object, e As EventArgs) Handles btnClearPoints.Click
        dataGrid.Rows.Clear()
        picMain.Image = Nothing

    End Sub

    ' Display the result in the PictureBox
    Private Sub DrawCircle(circle As Circle)
        ' Clear the PictureBox
        picMain.Image = Nothing
        Dim bmp As New Bitmap(picMain.Width, picMain.Height)

        Dim g As Graphics = Graphics.FromImage(bmp)
        g.TextRenderingHint = TextRenderingHint.AntiAlias
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

        Dim dx As Single = picMain.Width / 2
        Dim dy As Single = picMain.Height / 2

        ' Draw the origin point and X-Y coordinate lines
        g.DrawEllipse(Pens.Green, dx - 2, dy - 2, 4, 4)
        g.DrawLine(Pens.Black, 0, dy, picMain.Width, dy)
        g.DrawLine(Pens.Black, dx, 0, dx, picMain.Height)

        ' Draw the points
        Dim myFont As New Font("Seoge UI", 8)
        Dim brhBlue As New SolidBrush(Color.Blue)
        Dim brhBlack As New SolidBrush(Color.Black)

        For Each pt As PointS In points
            ' Draw the point
            g.FillEllipse(brhBlue, dx + pt.X - 2, dy - pt.Y - 2, 4, 4)

            ' Draw a X-Y coordinates for the point.
            g.DrawString("(" + pt.X.ToString + ", " + pt.Y.ToString + ")", myFont, brhBlack, dx + pt.X + 5, dy - pt.Y - 5)
        Next

        Dim centerX As Single = Math.Floor(circle.CenterX * 1000) / 1000
        Dim centerY As Single = Math.Floor(circle.CenterY * 1000) / 1000
        Dim radius As Single = Math.Floor(circle.Radius * 1000) / 1000

        ' Draw the best-fit circle
        ' Draw center of the circle
        Dim brhRed As New SolidBrush(Color.Red)
        g.FillEllipse(brhRed, dx + centerX - 2, dy - centerY - 2, 4, 4)
        g.DrawString("(" + centerX.ToString + ", " + centerY.ToString + ")", myFont,
                        brhBlack, dx + centerX + 5, dy - centerY - 5)
        ' Draw radius of the circle
        g.DrawString("Radius:" + radius.ToString, myFont, brhBlack, 10, 10)
        ' Draw the circle
        g.DrawEllipse(Pens.Red, dx + centerX - radius, dy - centerY - radius, 2 * radius, 2 * radius)

        ' Display the result
        picMain.Image = bmp
    End Sub

End Class

' Point class
Public Class PointS
    Public Property X As Single
    Public Property Y As Single
End Class

' Circle Class
Public Class Circle
    Public Property CenterX As Single
    Public Property CenterY As Single
    Public Property Radius As Single
End Class

Public Class CircleFit

    Public Shared Function FitCircle(points As List(Of PointS)) As Circle
        Dim n As Integer = points.Count

        ' Construct the matrix A and vector B for the linear system Ax = B
        Dim A As Matrix(Of Double) = Matrix(Of Double).Build.Dense(n, 3)
        Dim B As Vector(Of Double) = Vector(Of Double).Build.Dense(n)

        For i As Integer = 0 To n - 1
            A(i, 0) = 2 * points(i).X
            A(i, 1) = 2 * points(i).Y
            A(i, 2) = -1
            B(i) = points(i).X ^ 2 + points(i).Y ^ 2
        Next

        ' Solve the linear system using Math.NET Numerics
        Dim x As Vector(Of Double) = A.Svd().Solve(B)

        ' Calculate circle parameters
        Dim centerX As Double = x(0)
        Dim centerY As Double = x(1)
        Dim radius As Double = Math.Sqrt(centerX ^ 2 + centerY ^ 2 - x(2))

        Return New Circle With {.CenterX = CSng(centerX), .CenterY = CSng(centerY), .Radius = CSng(radius)}
    End Function

End Class

