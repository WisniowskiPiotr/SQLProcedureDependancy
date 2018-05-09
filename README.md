# SQLProcedureDependency
C# nuget package which allows for getting notifications when results of Sql Server stored procedure will change. This can be used to invalidate cache, get application or user notified about changes made by other users in DB. This is similar to SqlDependency or SqlDependencyEx but allows subscription for results of almost any stored procedure.

### Usage Example
Note: before using this library it is required to install sql objects. Please see *Sql Install* chapter.
#### Core Classes
Static Class DependencyDB allows to manage subscription receivers (get or add). Receiver class allows to subscribe for notification and handles incoming messages. Typical example:
```
private void HandleMessage(NotificationMessage message)
{
  // Do sth with message object containing inserted and deleted data along with subscription details.
}

public void SubscribeForProcedureNotification (string subscriberName)
{
  string connectionString = "Data Source=<ServerName>;Initial Catalog=<DBName>;UID=<InstalledMainServiceName>;PWD=<password>";
  string appName = "<InstalledMainServiceName>";
  string procedureSchemaName = "<procedureSchemaName>";
  string procedureName = "<procedureName>";
  Receiver receiver = DependencyDB.AddReceiver( appName, connectionString, HandleMessage);
  if (!receiver.IsListening())
  {
    new System.Threading.Thread(() =>
      {
        System.Threading.Thread.CurrentThread.IsBackground = true;
        receiver.Listen();
      }).Start();
  }
  receiver.Subscribe(subscriberName, procedureSchemaName, procedureName, null, DateTime.Now.AddDays(2));
}
```
After changes in tables on which procedure depends on and meeting conditions on which different  data would be returned from procedure, notification is being send by broker and handled by *HandleMessage* method.

#### Example: Notifying users with SignalR
Global.asax.cs:
```
public class Global : System.Web.HttpApplication
{
  class ListenerManager : IRegisteredObject
  {
    public void HandleMessage(NotificationMessage message)
      {
          GlobalHost.ConnectionManager.GetHubContext<ClientHub>().Clients.Client(message.SubscriberString).ClientRefreshPage(message.Inserted.ToString());
      }
    Receiver Listener;
    public ListenerManager()
    {
       jobpartsListener = DependencyDB.AddReceiver(
          "<AppName>",
          "<ConnectionString>",
          HandleMessage);
    }
    public void Stop(bool immediate)
    {
        Listener.Stop();
        HostingEnvironment.UnregisterObject(this);
    }
    public void Start()
    {
      HostingEnvironment.RegisterObject(this);
      HostingEnvironment.QueueBackgroundWorkItem((cancelationTocken) =>
      {
        Listener.Listen(cancelationTocken);
      });
    }
  }

  ListenerManager Manager;
  void Application_Start(object sender, EventArgs e)
  {
      if (ConfigurationManager.AppSettings["SqlProcedureDependency_Enabled"] == "true")
      {
          Manager = new ListenerManager();
          Manager.Start();
      }
  }
}
```
ClientHub.cs
```
// client methods // methods called by server on client browser - declaration only, implementation in Jquery
public interface IClient
{
  void ClientRefreshPage(string insertedDataXml);
}

// Methods send to client. // methods to be called by client from jquery
[HubName("ClientHub")]
public class ClientHub : Hub<IClient>
{
  /// <summary>
  /// Creates subscribe for changes in results of sql procedure.
  /// </summary>
  /// <param name="paramName"> Parameter used in query. </param>
  [HubMethodName("ServerRegisterForNotifications")]
  public void ServerRegisterForNotifications(int paramName = -1)
  {
      SqlCommand command = new SqlCommand("<procedureName>");
      command.Parameters.Add(AccessDB.CreateSqlParameter("@paramName", SqlDbType.Int, paramName));

      DependencyDB.GetReceiver("<appName>").Subscribe(
          Context.ConnectionId,
          "<appName>",
          command.CommandText,
          command.Parameters,
          DateTime.Now.AddHours(24)
          );
  }

  /// <summary>
  /// Unsubscripes from notifications.
  /// </summary>
  [HubMethodName("ServerUnsubscribeFromNotifications")]
  public void ServerUnsubscribeFromNotifications()
  {
      DependencyDB.GetReceiver("<appName>").UnSubscribe( Context.ConnectionId);
  }
}
```

### Sql Install
It is required to install and configure DB before using subscriptions. This can be done by running script generated by *AdminDependencyDB.GetAdminInstallScript* with admin privileges. This will create Sql Login, User, Schema and other required objects ([see this](./SQLProcedureDependancy/SQLScripts/AdminInstall.sql)). Optionally to allow this newly created user observe objects from other schemas please run *AdminDependencyDB.GetAdminGrantObservedShemaScript* to generate appropriate script.
```
string script = AdminDependencyDB.GetAdminInstallScript("<DbName>", "<AppName>", "<Password>", "<LoginName>");
File.WriteAllText("InstalScript.sql", script);
script = AdminDependencyDB.GetAdminUnInstallScript("<DbName>", "<AppName>", "<LoginName>");
File.WriteAllText("UnInstalScript.sql", script);
script = AdminDependencyDB.GetAdminGrantObservedShemaScript("<DbName>", "<AppName>", "<SchemaName>");
File.WriteAllText("GrantObservedShema.sql", script);
```
Alternatively all objects can be installed by using *(new AdminDependencyDB("<adminConnectionString>")).AdminInstall(...)* and other similar methods.
This needs to be done only once and doing so in production code is highly discouraged.
  
### Contact

If You need more examples please let me know. Note this code is already being used in production.
If You have any questions or problems contact me at [contact.wisniowskipiotr@gmail.com](mailto:contact.wisniowskipiotr@gmail.com).
