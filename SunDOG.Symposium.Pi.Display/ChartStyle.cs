namespace SunDOG.Symposium.Pi.Display
{
    public class ChartStyle
    {

        // --- COLORS ---
        public string PrimaryColor { get; set; } = "#FBE3D6";
        public string SecondaryColor { get; set; } = "#ff7f0e";
        public string GridLineColor { get; set; } = "#0E0A1F";
        public string AxisTitleColor { get; set; } = "#0E0A1F";

        // --- STROKES ---
        public double LineWidth { get; set; } = 2.0;
        public string GridLineDashArray { get; set; } = "5, 5";

        // --- FONTS ---
        public string FontFamily { get; set; } = "Arial, sans-serif";
        public double TitleFontSize { get; set; } = 25.0;
        public double LabelFontSize { get; set; } = 18.0;

        // --- SPACING ---
        public int AxisPadding { get; set; } = 20;

    }
}
