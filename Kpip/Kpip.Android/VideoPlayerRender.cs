using System;
using Android.App;
using Android.Content;
using Android.Media;
using Android.Util;
using Android.Views;
using Android.Widget;
using Kpip;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Android.Widget.Button;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(VideoPlayerView), typeof(Kpip.Droid.VideoPlayerRender))]
namespace Kpip.Droid
{
    public class VideoPlayerRender : ViewRenderer<VideoPlayerView, Android.Widget.RelativeLayout>
    {
        Context context;

       
        VideoView videoView;

        public MediaPlayer player;
        public LinearLayout layaoutplayer;
        Button button;
        /** The arguments to be used for Picture-in-Picture mode. */
        PictureInPictureParams.Builder pictureInPictureParamsBuilder = new PictureInPictureParams.Builder();

        public VideoPlayerRender(Context _context) : base(_context)
        {
            this.context = _context;

            //Inflate(context, Resource.Layout.Player, this);

        }
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayerView> e)
        {
            base.OnElementChanged(e);
            if(e.NewElement != null)
            {
                player = new MediaPlayer();

                if (Control == null)
                {
                    videoView = new VideoView(Context);
                    Android.Widget.RelativeLayout f = new Android.Widget.RelativeLayout(context);

                    Button v = new Button(context);
                    Button pip = new Button(context);
                    pip.Text = "pip";
                    v.Text = "pause";
                    v.Click += delegate {
                        if (videoView.IsPlaying)
                        {
                            videoView.Pause();
                            v.Text = "play";
                        }
                        else
                        {
                            videoView.Start();
                            v.Text = "pause";

                        }

                    };
                    pip.Click += delegate {
                        Minimize();


                    };
                   
                    f.AddView(v);
              
                    
                    f.AddView(videoView);
                    f.AddView(pip);
                    pip.TranslationX = 300;
                    
                   
                    var uri = Android.Net.Uri.Parse("https://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
                    videoView.SetVideoURI(uri);
                    videoView.Start();

                    
                    SetNativeControl(f);

                }

            }
           

        }

          public void Minimize()
        {

            var aspectRatio = new Rational(videoView.Width, videoView.Height);
            pictureInPictureParamsBuilder.SetAspectRatio(aspectRatio).Build();
            context.GetActivity().EnterPictureInPictureMode(pictureInPictureParamsBuilder.Build());
            
            return;
        }



    }
}
