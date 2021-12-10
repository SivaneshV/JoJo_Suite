Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading

Public Class r2rSAPContainer

    Private _guiExeLocation As String
    Private _sysId As String
    Private _client As String
    Private _lang As String
    Private _error As Boolean = True
    Private _errorMsg As String = "DoAction() method not called"
    Private _sapGuiApp
    Private _conn
    'Private _sapGuiConn As GuiConnection
    Private _sapGuiSession As Object

    Public Property GuiAppLocation As String
        Get
            Return _guiExeLocation
        End Get

        Set(ByVal value As String)
            _guiExeLocation = value
        End Set
    End Property

    Public Property SysID As String
        Get
            Return _sysId
        End Get

        Set(ByVal value As String)
            _sysId = value
        End Set
    End Property

    Public Property Client As String
        Get
            Return _client
        End Get

        Set(ByVal value As String)
            _client = value
        End Set
    End Property


    Public Property Language As String
        Get
            Return _lang
        End Get

        Set(ByVal value As String)
            _lang = value
        End Set
    End Property

    'Public Property GuiApplication As GuiApplication
    '    Get
    '        Return _sapGuiApp
    '    End Get
    'End Property

    'Public Property GuiConnection As GuiConnection
    '    Get
    '        Return _sapGuiConn
    '    End Get
    'End Property

    Public ReadOnly Property GuiSession As Object
        Get
            Return _sapGuiSession
        End Get
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
            '_sapGuiApp = New GuiApplication()
            '_sapGuiConn = _sapGuiApp.OpenConnection(_sysId, Sync:=True)
            '_sapGuiSession = CType(_sapGuiConn.Sessions.Item(0), GuiSession)
            Process.Start(GuiAppLocation)
            Thread.Sleep(5000)
            _sapGuiApp = GetObject("SAPGUI").GetscriptingEngine
            _conn = _sapGuiApp.OpenConnection(SysID, False)
            _sapGuiSession = _conn.Children(0)
            _sapGuiSession.FindById("wnd[0]").maximize()
            _error = False
            _errorMsg = ""
            res = True
        Catch ex As Exception
            res = False
            _error = True
            _errorMsg = Me.[GetType]().ToString() & ":" & vbLf + ex.Message
        End Try
        Return res
    End Function
End Class
