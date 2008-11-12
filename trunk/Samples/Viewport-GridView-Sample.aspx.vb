'
' Ra-Ajax - A Managed Ajax Library for ASP.NET and Mono
' Copyright 2008 - Thomas Hansen thomas@ra-ajax.org
' This code is licensed under the LGPL version 3 which 
' can be found in the license.txt file on disc.
' 


Imports System
Imports Ra.Widgets
Imports Ra.Extensions

Namespace Samples
    Public Partial Class ViewportGridView
        Inherits System.Web.UI.Page
        Private _triedToLogin As Boolean
        
        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
            ' Remove this line, just here for "testing purposes" to make it easy to "log out"
            If Not IsPostBack Then
                Session("user") = Nothing
                grid.DataSource = PeopleDatabase.Database
                grid.DataBind()
            End If
        End Sub
        
        ' We want to wait as long as possible with this logic to make sure we're NOT
        ' running this logic e.e. when user is actually logging in etc...
        Protected Overloads Overrides Sub OnPreRender(ByVal e As EventArgs)
            If Session("user") Is Nothing AndAlso Not _triedToLogin AndAlso Not loginWnd.Visible Then
                ' Uncomment the line below to have "true" login logic...
                ' This line is commented just to make everything show for "bling reasons"...
                'everything.Visible = false;
                loginWnd.Visible = True
                Dim effect as Effect = New EffectFadeIn(loginWnd, 1000)
                effect.Render()
                username.Text = "username"
                password.Text = "password"
                username.Select()
                username.Focus()
            End If
            MyBase.OnPreRender(e)
        End Sub
        
        Protected Sub resizer_Resized(ByVal sender As Object, ByVal e As ResizeHandler.ResizedEventArgs)
            lbl.Text = String.Format("Width: {0}, Height: {1}", e.Width, e.Height)
            Dim effect = New EffectHighlight(lbl, 500)
            effect.Render()
            Dim width As Integer = Math.Max(e.Width - 264, 400)
            Dim height As Integer = Math.Max(e.Height - 101, 200)
            Dim heightLeft As Integer = Math.Max(e.Height - 390, 50)
            wndRight.Style("width") = width.ToString() & "px"
            pnlRight.Style("height") = height.ToString() & "px"
            pnlLeft.Style("height") = heightLeft.ToString() & "px"
        End Sub
        
        Protected Sub loginBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
            _triedToLogin = True
            If username.Text = "admin" And password.Text = "admin" Then
                loginWnd.Visible = False
                everything.Visible = True
                Session("user") = "admin"
            Else
                loginInfo.Text = "Username; <strong>'admin'</strong>, Password; <strong>'admin'</strong>"
                Dim effect = New EffectHighlight(loginInfo, 500)
                effect.Render()
                username.Select()
                username.Focus()
            End If
        End Sub
    End Class
End Namespace
