using System.Text;
using Windows.Media;
using Windows.Media.Core;
using Windows.Media.Streaming.Adaptive;
using Windows.Storage.Streams;

namespace BiliStart.Helpers;
public static class PlayerHelper
{
    public static async Task<MediaSource> CreateMediaSourceAsync(BiliBiliAPI.Models.Videos.DashVideo Video, BiliBiliAPI.Models.Videos.DashVideo Audio)
    {
        try
        {
            Windows.Web.Http.HttpClient httpClient = new();
            httpClient.DefaultRequestHeaders.Referer = new Uri("https://www.bilibili.com");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/69.0.3497.100 Safari/537.36");
            var mpdStr = $@"<MPD xmlns=""urn:mpeg:DASH:schema:MPD:2011""  profiles=""urn:mpeg:dash:profile:isoff-on-demand:2011"" type=""static"">
                                  <Period  start=""PT0S"">
                                    <AdaptationSet>
                                      <ContentComponent contentType=""video"" id=""1"" />
                                      <Representation bandwidth=""{Video.BandWidth}"" codecs=""{Video.Codecs}"" height=""{Video.Height}"" id=""{Video.ID}"" mimeType=""{Video.VideoType}"" width=""{Video.Width}"">
                                        <BaseURL></BaseURL>
                                        <SegmentBase indexRange=""{Video.SegmentBase.indexRange}"">
                                          <Initialization range=""{Video.SegmentBase.Initialization}"" />
                                        </SegmentBase>
                                      </Representation>
                                    </AdaptationSet>
                                    {{audio}}
                                  </Period>
                                </MPD>
                                ";
            if (Audio == null)
                mpdStr = mpdStr.Replace("{audio}", "");
            else
                mpdStr = mpdStr.Replace("{audio}", $@"<AdaptationSet>
                                      <ContentComponent contentType=""audio"" id=""2"" />
                                      <Representation bandwidth=""{Audio.BandWidth}"" codecs=""{Audio.Codecs}"" id=""{Audio.ID}"" mimeType=""{Audio.VideoType}"" >
                                        <BaseURL></BaseURL>
                                        <SegmentBase indexRange=""{Audio.SegmentBase.indexRange}"">
                                          <Initialization range=""{Audio.SegmentBase.Initialization}"" />
                                        </SegmentBase>
                                      </Representation>
                                    </AdaptationSet>");
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(mpdStr)).AsInputStream();
            var soure = await AdaptiveMediaSource.CreateFromStreamAsync(stream, new Uri(Video.BaseUrl), "application/dash+xml", httpClient);
            var s = soure.Status;
            soure.MediaSource.DownloadRequested += (sender, args) =>
            {
                if (args.ResourceContentType == "audio/mp4" && Audio != null)
                {
                    args.Result.ResourceUri = new Uri(Audio.Base_Url);
                }
            };
            return MediaSource.CreateFromAdaptiveMediaSource(soure.MediaSource);
        }
        catch (Exception)
        {
            return null;
        }

    }


    
}
