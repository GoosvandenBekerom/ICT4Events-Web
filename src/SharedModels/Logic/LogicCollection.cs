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

        private static EventLogic _eventLogic;
        public static EventLogic EventLogic => _eventLogic ?? (_eventLogic = new EventLogic(new EventOracleContext()));

        private static LocationLogic _locationLogic;
        public static LocationLogic LocationLogic => _locationLogic ?? (_locationLogic = new LocationLogic(new LocationOracleContext()));

        private static PlaceLogic _placeLogic;
        public static PlaceLogic PlaceLogic => _placeLogic ?? (_placeLogic = new PlaceLogic(new PlaceOracleContext()));

        private static PersonLogic _personLogic;
        public static PersonLogic PersonLogic => _personLogic ?? (_personLogic = new PersonLogic(new PersonOracleContext()));

        private static ReservationLogic _reservationLogic;
        public static ReservationLogic ReservationLogic => _reservationLogic ?? (_reservationLogic = new ReservationLogic(new ReservationOracleContext()));
    }
}
