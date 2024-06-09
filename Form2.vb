Imports System.Net

Public Class Form2
    Private _form1 As Form1
    Private Cookies As New List(Of Cookie)

    Public Sub New(form1 As Form1)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _form1 = form1

        ' Initialize cookies
        Cookies.Add(New Cookie("Tart Nenas", 10))
        Cookies.Add(New Cookie("Semperit", 12))
        Cookies.Add(New Cookie("Tart Cadbury", 18))
        Cookies.Add(New Cookie("Fluorentine", 15))
    End Sub

    Public Sub UpdateLabelText()
        'Update the list of selected cookies and their quantity
        Dim labelText As String = ""
        Dim quantityText As String = ""

        For Each cookie In Cookies
            If _form1.IsCookieChecked(cookie.Name) Then
                labelText &= "-" & cookie.Name & vbCrLf
                quantityText &= "-" & cookie.Name & ": " & _form1.GetCookieQuantity(cookie.Name) & vbCrLf
            End If
        Next

        'Update the label on the form
        lblCookiesName.Text = labelText.Trim()
        lblQuantity.Text = quantityText.Trim()
    End Sub


    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub btnHome_Click(sender As Object, e As EventArgs) Handles btnHome.Click
        _form1.Show()
        Me.Close()
    End Sub

    Private Sub btnPay_Click(sender As Object, e As EventArgs) Handles btnPay.Click
        'Validate user inputs
        If lblCookiesName.Text = "" Then
            MessageBox.Show("Please select cookies first", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        If tbName.Text = "" OrElse tbAddress.Text = "" Then
            MessageBox.Show("Please enter your name and address", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        If cbDeli.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a delivery method", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        If cbType.SelectedItem Is Nothing Then
            MessageBox.Show("Please select a cookie type", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

        'Calculate total cost
        Dim totalCost As Decimal = CalculateTotalCost()

        'Display order confirmation
        DisplayOrderConfirmation(totalCost)
    End Sub

    Private Function CalculateTotalCost() As Decimal
        'Constants for price per unit and postage cost
        Const originalPerUnit As Decimal = 19
        Const MiniSizePricePerUnit As Decimal = 15
        Const PostageCost As Decimal = 8

        'Initialize total cost
        Dim totalCost As Decimal = 0

        'Determine the price per unit based on the cookie type
        Dim pricePerUnit As Decimal = If(cbType.SelectedItem IsNot Nothing AndAlso cbType.SelectedItem.ToString() = "Mini Size (RM 15 /15 pcs)", MiniSizePricePerUnit, originalPerUnit)

        'Iterate through each selected cookie
        For Each cookie In Cookies
            If _form1.IsCookieChecked(cookie.Name) Then
                'Add price per unit to total cost for each selected cookie
                totalCost += pricePerUnit * _form1.GetCookieQuantity(cookie.Name)
            End If
        Next

        'Add postage cost if applicable
        If cbDeli.SelectedItem IsNot Nothing AndAlso cbDeli.SelectedItem.ToString() = "Postage (RM 8)" Then
            totalCost += PostageCost
        End If

        Return totalCost
    End Function

    Private Sub DisplayOrderConfirmation(totalCost As Decimal)
        'Construct confirmation message
        Dim confirmationMessage As String = "Name :" & tbName.Text & vbCrLf & "Address :" & tbAddress.Text & vbCrLf & "Type :" & cbType.SelectedItem.ToString() & vbCrLf & "Delivery :" & cbDeli.SelectedItem.ToString() & vbCrLf & "Cookies :" & vbCrLf & lblCookiesName.Text & vbCrLf & "Quantities : " & vbCrLf & lblQuantity.Text & vbCrLf & "Total Cost : RM" & totalCost.ToString("0.00")

        'Display order confirmation message
        MessageBox.Show(confirmationMessage, "Order Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information)

        'Set the confirmation message to the display label
        lblDisplay.Text = confirmationMessage
    End Sub

End Class

Public Class Cookie
    Public Property Name As String

    Public Property Price As Decimal

    Public Property Quantity As Integer

    Public Sub New(name As String, price As Decimal)
        'Initialize cookie properties
        Me.Name = name
        Me.Price = price
        Me.Quantity = 0 ' Defauult quantity is 0
    End Sub
End Class