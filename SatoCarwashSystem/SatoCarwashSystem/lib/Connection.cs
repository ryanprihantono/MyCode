using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using SatoCarwashSystem.connect;
using SatoCarwashSystem.robotic;

namespace SatoCarwashSystem.lib
{
    public static class Connection
    {
        public static String dbConnectionString;
        public static String serverAddress;
        public static SocketClient socketClient;
        public static QueryLog queryLog;
        public static OPCServerPCAccess opc;
    }
}
