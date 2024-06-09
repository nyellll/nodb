Public Class Form1
    Private Sub btnOrder_Click(sender As Object, e As EventArgs) Handles btnOrder.Click
        Dim f2 As New Form2(Me)
        f2.UpdateLabelText()
        f2.Show()
        Me.Hide()
    End Sub
End Class
