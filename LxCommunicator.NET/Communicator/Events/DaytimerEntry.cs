namespace Loxone.Communicator.Events
{
    public class DayTimerEntry
    {
        /// <summary>
        /// Number of current mode.
        /// </summary>
        public int Mode { get; private set; }

        /// <summary>
        /// From-time in minutes since midnight.
        /// </summary>
        public int From { get; private set; }

        /// <summary>
        /// To-time in minutes since midnight.
        /// </summary>
        public int To { get; private set; }

        /// <summary>
        /// Trigger.
        /// </summary>
        public int NeedActivate { get; private set; }

        /// <summary>
        /// Value (if analog Day Timer).
        /// </summary>
        public double Value { get; private set; }

        /// <summary>
        /// Reads the next <see cref="DayTimerEntry"/> of a BinaryReader.
        /// </summary>
        /// <param name="reader">The binaryReader that should be read.</param>
        /// <returns>The <see cref="DayTimerEntry"/>.</returns>
        public static DayTimerEntry Parse(System.IO.BinaryReader reader)
        {
            DayTimerEntry entry = new DayTimerEntry
            {
                Mode = reader.ReadInt32(),
                From = reader.ReadInt32(),
                To = reader.ReadInt32(),
                NeedActivate = reader.ReadInt32(),
                Value = reader.ReadDouble()
            };

            return entry;
        }
    }
}
