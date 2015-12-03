using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedModels.Data.OracleContexts;

namespace SharedModels.Logic
{
    /// <summary>
    /// Helper class used for accessing the user logic globally
    /// Currently defaults to using Oracle contexts
    /// </summary>
    public static class LogicCollection
    {
        private static UserLogic _userLogic;
        public static UserLogic UserLogic => _userLogic ?? (_userLogic = new UserLogic(new UserOracleContext()));

        private static GuestLogic _guestLogic;
        public static GuestLogic GuestLogic => _guestLogic ?? (_guestLogic = new GuestLogic(new GuestOracleContext()));

        private static EventLogic _eventLogic;
        public static EventLogic EventLogic => _eventLogic ?? (_eventLogic = new EventLogic(new EventOracleContext()));

        private static LocationLogic _locationLogic;
        public static LocationLogic LocationLogic => _locationLogic ?? (_locationLogic = new LocationLogic(new LocationOracleContext()));

        private static PostLogic _postLogic;
        public static PostLogic PostLogic => _postLogic ?? (_postLogic = new PostLogic(new PostOracleContext()));

        private static MaterialLogic _materialLogic;
        public static MaterialLogic MaterialLogic => _materialLogic ?? (_materialLogic = new MaterialLogic( new MaterialOracleContext()));

        private static MediaLogic _mediaLogic;
        public static MediaLogic MediaLogic => _mediaLogic ?? (_mediaLogic = new MediaLogic(new MediaOracleContext()));
    }
}
