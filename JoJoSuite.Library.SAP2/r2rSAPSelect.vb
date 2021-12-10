Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Public Class r2rSAPSelect
    Private _controlId As String

    'Private _value As String

    Private _sapGuiSession As Object

    Private ControlType As String

    Private _Catagory As String
    Private _SelectTab As Boolean
    Private _SelectBox As Boolean
    Private _ChooseText As String

    Private _error As Boolean = True

    Private _errorMsg As String = "DoAction() method not called"

    Public Property ControlID As String
        Get
            Return _controlId
        End Get

        Set(ByVal value As String)
            _controlId = value
        End Set
    End Property

    Public Property Catagory As String
        Get
            Return _Catagory
        End Get

        Set(ByVal value As String)
            _Catagory = value
        End Set
    End Property
    Public Property ChooseText As String
        Get
            Return _ChooseText
        End Get

        Set(ByVal value As String)
            _ChooseText = value
        End Set
    End Property
    Public Property SelectBox As Boolean
        Get
            Return _SelectBox
        End Get

        Set(ByVal value As Boolean)
            _SelectBox = value
        End Set
    End Property
    Public Property SelectTab As Boolean
        Get
            Return _SelectTab
        End Get

        Set(ByVal value As Boolean)
            _SelectTab = value
        End Set
    End Property

    Public Property SAPGuiSession As Object
        Get
            Return _sapGuiSession
        End Get

        Set(ByVal value As Object)
            _sapGuiSession = value
        End Set
    End Property

    Public ReadOnly Property [Error] As Boolean
        Get
            Return _error
        End Get
    End Property

    Public ReadOnly Property ErrorMessage As String
        Get
            Return _errorMsg
        End Get
    End Property

    Public Function DoAction() As Boolean
        Dim res As Boolean = False
        Try
            If Catagory = "DropDown" Then
                ''Threading.Thread.Sleep(2000)
                _sapGuiSession.FindById(ControlID).key = _ChooseText
            ElseIf Catagory = "SelectTab" Then
                ''Threading.Thread.Sleep(2000)
                _sapGuiSession.FindById(ControlID).select()
            ElseIf Catagory = "CheckBox" Then
                ''Threading.Thread.Sleep(2000)
                _sapGuiSession.FindById(ControlID).selected = _SelectBox
            End If


            _error = False
            _errorMsg = ""
            res = True
        Catch ex As Exception
            res = False
            _error = True
            _errorMsg = [GetType]().ToString() & ":" & vbLf + ex.Message
        End Try

        Return res
    End Function
End Class
