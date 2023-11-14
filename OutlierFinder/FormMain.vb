﻿Imports System.Drawing
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

        FindAndDrawCircle()
    End Sub

    Private Sub btnClearPoints_Click(sender As Object, e As EventArgs) Handles btnClearPoints.Click
        dataGrid.Rows.Clear()
        picMain.Image = Nothing
    End Sub

    '----------------------------------------------------------------------------'
    ' Find and draw best fit circle
    '----------------------------------------------------------------------------'
    Private Sub btnFindBFC_Click(sender As Object, e As EventArgs) Handles btnFindBFC.Click
        FindAndDrawCircle()
    End Sub

    Private Sub FindAndDrawCircle()
        UpdatePointsFromDataGrid()

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

    ' Display the result for Best Fit Circle in the PictureBox
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
        Dim fontNormal As New Font("Seoge UI", 8)
        Dim fontBig As New Font("Seoge UI", 14)
        Dim brhBlue As New SolidBrush(Color.Blue)
        Dim brhBlack As New SolidBrush(Color.Black)

        For Each pt As PointS In points
            ' Draw the point
            g.FillEllipse(brhBlue, dx + pt.X - 2, dy - pt.Y - 2, 4, 4)

            ' Draw a X-Y coordinates for the point.
            g.DrawString("(" + pt.X.ToString + ", " + pt.Y.ToString + ")", fontNormal, brhBlack, dx + pt.X + 5, dy - pt.Y - 5)
        Next

        Dim centerX As Single = Math.Floor(circle.CenterX * 1000) / 1000
        Dim centerY As Single = Math.Floor(circle.CenterY * 1000) / 1000
        Dim radius As Single = Math.Floor(circle.Radius * 1000) / 1000

        ' Draw the best-fit circle
        ' Draw center of the circle
        Dim brhRed As New SolidBrush(Color.Red)
        g.FillEllipse(brhRed, dx + centerX - 2, dy - centerY - 2, 4, 4)
        g.DrawString("(" + centerX.ToString + ", " + centerY.ToString + ")", fontNormal,
                        brhBlack, dx + centerX + 5, dy - centerY - 5)
        ' Draw radius of the circle
        g.DrawString("Radius:" + radius.ToString, fontBig, brhBlack, 10, 10)
        ' Draw the circle
        g.DrawEllipse(Pens.Red, dx + centerX - radius, dy - centerY - radius, 2 * radius, 2 * radius)

        ' Display the result
        picMain.Image = bmp
    End Sub

    '----------------------------------------------------------------------------'
    ' Find and draw trend line
    '----------------------------------------------------------------------------'
    Private Sub btnFindTrendLine_Click(sender As Object, e As EventArgs) Handles btnFindTrendLine.Click
        FindTrendLine()
    End Sub

    Private Sub FindTrendLine()

        UpdatePointsFromDataGrid()

        ' Use the Least Squares method to find the best-fit circle
        Dim lr As LinearRegression = TrendLineFinder.Calculate(points)

        ' Calculate the minimum distance of each point to circle
        For Each row As DataGridViewRow In dataGrid.Rows
            If row.Index >= dataGrid.Rows.Count - 1 Then
                Exit For
            End If

            Dim pt As PointS = points(row.Index)
            row.Cells(2).Value = CSng(Math.Abs(lr.Slope * pt.X - pt.Y + lr.Intercept) / Math.Sqrt(lr.Slope ^ 2 + 1))
        Next

        ' Draw the best fit circle
        DrawTrendLine(lr)
    End Sub

    ' Display the result for Best Fit Circle in the PictureBox
    Private Sub DrawTrendLine(lr As LinearRegression)
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
        Dim fontNormal As New Font("Seoge UI", 8)
        Dim fontBig As New Font("Seoge UI", 14)
        Dim brhBlue As New SolidBrush(Color.Blue)
        Dim brhRed As New SolidBrush(Color.Red)
        Dim brhBlack As New SolidBrush(Color.Black)

        For Each pt As PointS In points
            ' Draw the point
            g.FillEllipse(brhBlue, dx + pt.X - 3, dy - pt.Y - 3, 6, 6)

            ' Draw a X-Y coordinates for the point.
            g.DrawString("(" + pt.X.ToString + ", " + pt.Y.ToString + ")", fontNormal, brhBlack, dx + pt.X + 5, dy - pt.Y - 5)
        Next

        ' Draw the regression equation
        g.DrawString("y = " + CSng(lr.Slope).ToString + "x " + If(lr.Intercept > 0, "+ ", "") + CSng(lr.Intercept).ToString, fontBig, brhBlack, 10, 10)

        ' Draw the trend line
        Dim xStart As Single = -picMain.Width / 2
        Dim yStart As Single = CSng(lr.Slope * xStart + lr.Intercept)
        Dim xEnd As Single = picMain.Width / 2
        Dim yEnd As Single = CSng(lr.Slope * xEnd + lr.Intercept)
        g.DrawLine(New Pen(Color.Green, 2), dx + xStart, dy - yStart, dx + xEnd, dy - yEnd)

        ' Draw the line from each point to its nearest point on trend line
        For Each pt As PointS In points
            Dim xNearest As Double = (pt.X + lr.Slope * pt.Y - lr.Slope * lr.Intercept) / (lr.Slope ^ 2 + 1)
            Dim yNearest As Double = lr.Slope * xNearest + lr.Intercept

            ' Draw the nearest point on the trend line
            g.FillEllipse(brhRed, CSng(dx + xNearest - 3), CSng(dy - yNearest - 3), 6, 6)

            ' Draw the line between two points
            g.DrawLine(Pens.Red, CSng(dx + pt.X), CSng(dy - pt.Y), CSng(dx + xNearest), CSng(dy - yNearest))
        Next

        ' Display the result
        picMain.Image = bmp
    End Sub

    Private Sub UpdatePointsFromDataGrid()
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

Public Class LinearRegression
    Public Property Slope As Double
    Public Property Intercept As Double
End Class

Public Class CircleFit

    ' Use the Least Squares method to find the best-fit circle
    Public Shared Function FitCircle(points As List(Of PointS)) As Circle
        If points Is Nothing OrElse points.Count < 2 Then
            Throw New ArgumentException("At least two points are required for best fit circle.")
        End If

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


Public Class TrendLineFinder

    Public Shared Function Calculate(ByVal points As List(Of PointS)) As LinearRegression
        If points Is Nothing OrElse points.Count < 2 Then
            Throw New ArgumentException("At least two points are required for linear regression.")
        End If

        Dim n As Integer = points.Count
        Dim sumX As Double = 0
        Dim sumY As Double = 0
        Dim sumXY As Double = 0
        Dim sumX2 As Double = 0

        For Each point In points
            sumX += point.X
            sumY += point.Y
            sumXY += point.X * point.Y
            sumX2 += point.X * point.X
        Next

        Dim Slope As Double = (n * sumXY - sumX * sumY) / (n * sumX2 - sumX * sumX)
        Dim Intercept As Double = (sumY - Slope * sumX) / n

        Return New LinearRegression With {.Slope = Slope, .Intercept = Intercept}
    End Function
End Class
