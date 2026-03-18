namespace SunDOG.Symposium.Pi.Display
{
    public class ChartStyle
    {
        // --- POSTER COLORS ---
        public string BackgroundColor { get; set; } = "#F5F6FD";
        public string Black { get; set; } = "#0E0A1F";
        public string DarkAccent { get; set; } = "#493C53";
        public string LightAccent { get; set; } = "#437F9B";

        // --- CHART MAPPING ---
        public string PrimaryColor { get; set; } = "#437F9B";

        public string SecondaryColor { get; set; } = "#0E0A1F";
        public string GridLineColor { get; set; } = "#493C53";
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