using System.Collections.Generic;

namespace Alpex.Measure;

public sealed partial class MeasureUnit
{
    public partial class WellKnown
    {
        // =========================================

        public static IReadOnlyDictionary<string, MeasureUnit> GetDictionary()
        {
            var dict = new Dictionary<string, MeasureUnit>();
            dict["Ω"]  = ResistanceOhm;
            dict["kΩ"] = ResistanceKiloOhm;
            dict["MΩ"] = ResistanceMegaOhm;
            dict["GΩ"] = ResistanceGigaOhm;
            dict["V"]  = VoltageVolt;
            dict["mV"] = VoltageMiliVolt;
            return dict;
        }

        public static MeasureUnit ResistanceOhm => new(MeasuredValue.Resistance, "Ω", 1m);

        public static MeasureUnit ResistanceKiloOhm => new(MeasuredValue.Resistance, "kΩ", 1000m);

        public static MeasureUnit ResistanceMegaOhm => new(MeasuredValue.Resistance, "MΩ", 1000_000m);

        public static MeasureUnit ResistanceGigaOhm => new(MeasuredValue.Resistance, "GΩ", 1000_000_000m);

        public static MeasureUnit VoltageVolt => new(MeasuredValue.Voltage, "V", 1m);

        public static MeasureUnit VoltageMiliVolt => new(MeasuredValue.Voltage, "mV", 0.001m);
    }
}
