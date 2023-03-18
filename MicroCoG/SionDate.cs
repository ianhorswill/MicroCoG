using System.Diagnostics;
using System.Globalization;

namespace MicroCoG
{
    [DebuggerDisplay("{DebugName}")]
    public readonly struct SionDate
    {
        public readonly int Days;

        public SionDate(int days)
        {
            Days = days;
        }

        private DateTime DateTime
        {
            get
            {
                var year = Days / 365;
                var dayOfYear = Days % 365;
                return new DateTime(year, 1, 1) + new TimeSpan(dayOfYear, 0, 0, 0);
            }
        }

        public override string ToString() => DateTime.ToString("MMMM dd, yyyy", CultureInfo.CurrentCulture);

        private string DebugName => ToString();

        public static explicit operator SionDate(int days) => new SionDate(days);
    }
}
