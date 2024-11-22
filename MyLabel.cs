#if ANDROID
using Android.Content;
using Android.Graphics;
using AndroidX.AppCompat.Widget;
#endif

using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;

namespace shell_issues;

public class MyLabel : Label
{
}

#if ANDROID
public class MyLabelHandler : LabelHandler
{
    private class MyLabelPlatform : MauiTextView
    {
        public MyLabelPlatform(Context context) : base(context)
        {
        }

        public override void Draw(Canvas canvas)
        {
            Console.WriteLine("MyLabelDraw");
            base.Draw(canvas);
        }

        protected override void OnLayout(bool changed, int left, int top, int right, int bottom)
        {
            Console.WriteLine("MyLabelOnLayout");
            base.OnLayout(changed, left, top, right, bottom);
        }

        public override void RequestLayout()
        {
            Console.WriteLine("MyLabelRequestLayout");
            base.RequestLayout();
        }
    }
    
    protected override AppCompatTextView CreatePlatformView()
    {
        return new MyLabelPlatform(Context);
    }
}
#endif