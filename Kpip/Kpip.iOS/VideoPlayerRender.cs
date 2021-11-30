using System;
using AVFoundation;
using AVKit;
using Foundation;
using Kpip;
using Kpip.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(VideoPlayerView), typeof(Kpip.iOS.VideoPlayerRender))]
namespace Kpip.iOS
{
    public class VideoPlayerRender : ViewRenderer<VideoPlayerView, UIView>
    {
        AVPlayer _player;
        AVPlayerViewController _playerViewController;
        VideoPlayerView _videoPlayer;

        public override UIViewController ViewController => _playerViewController;

        public VideoPlayerRender()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayerView> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                if(Control == null)
                {
                    // create AVPlayerViewController
                    _playerViewController = new AVPlayerViewController() { };

                    // set Player property to AVPlayer
                    _player = new AVPlayer(new NSUrl(Element.Source));
                    _playerViewController.Player = _player;

                    // use the View from the controller as the native control
                    SetNativeControl(_playerViewController.View);
                }

                _videoPlayer = e.NewElement;
            }

        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (_player != null)
            {
                _player.ReplaceCurrentItemWithPlayerItem(null);
            }
        }
    }
}
