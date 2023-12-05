using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;

namespace Veloci.Logic.Services;

public class ImageService
{
    public async Task<Stream> CreateWinnerImageAsync(string season, string winnerName)
    {
        const string templateName = "winner-template.png";

        // Season text
        const int seasonTextContainerWidth = 459;
        const int seasonTextContainerX = 0;
        const int seasonTextContainerY = 227;
        const int seasonFontSize = 24;

        // Winner text
        const int winnerTextContainerWidth = 399;
        const int winnerTextContainerX = 30;
        const int winnerTextContainerY = 465;
        const int winnerFontSize = 55;

        const string fontFamily = "Tahoma";


        var templatePath = Path.Combine(Environment.CurrentDirectory, $"wwwroot/images/{templateName}");
        using var template = await Image.LoadAsync(templatePath);

        var seasonfont = SystemFonts.CreateFont(fontFamily, seasonFontSize, FontStyle.Regular);
        var seasonOptions = new RichTextOptions(seasonfont)
        {
            Origin = new PointF(seasonTextContainerX, seasonTextContainerY),
            WrappingLength = seasonTextContainerWidth,
            TextAlignment = TextAlignment.Center,
        };

        var winnerfont = SystemFonts.CreateFont(fontFamily, winnerFontSize, FontStyle.Regular);
        var winnerOptions = new RichTextOptions(winnerfont)
        {
            Origin = new PointF(winnerTextContainerX, winnerTextContainerY),
            WrappingLength = winnerTextContainerWidth,
            TextAlignment = TextAlignment.Center,
        };

        var brush = Brushes.Solid(Color.White);
        template.Mutate(x => x
            .DrawText(seasonOptions, season, brush)
            .DrawText(winnerOptions, winnerName, brush));

        var stream = new MemoryStream();
        await template.SaveAsync(stream, new PngEncoder());

        return stream;
    }
}
