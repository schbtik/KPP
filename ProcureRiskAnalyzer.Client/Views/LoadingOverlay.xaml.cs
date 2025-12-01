using Microsoft.Maui.Controls;
using System.Diagnostics;

namespace ProcureRiskAnalyzer.Client.Views;

public partial class LoadingOverlay : ContentView
{
    private CancellationTokenSource? _animationCancellation;

    public static readonly BindableProperty IsVisibleProperty = 
        BindableProperty.Create(nameof(IsVisible), typeof(bool), typeof(LoadingOverlay), false, 
            propertyChanged: OnIsVisibleChanged);

    public bool IsVisible
    {
        get => (bool)GetValue(IsVisibleProperty);
        set
        {
            Debug.WriteLine($"[LoadingOverlay] IsVisible setter called with: {value}");
            SetValue(IsVisibleProperty, value);
        }
    }

    public LoadingOverlay()
    {
        InitializeComponent();
        // Don't set BindingContext = this, so that binding can find IsLoading in parent ViewModel
        overlayGrid.IsVisible = false; // Start hidden
        Debug.WriteLine("[LoadingOverlay] Constructor - overlayGrid.IsVisible set to false");
    }

    private static void OnIsVisibleChanged(BindableObject bindable, object oldValue, object newValue)
    {
        Debug.WriteLine($"[LoadingOverlay] OnIsVisibleChanged - oldValue: {oldValue}, newValue: {newValue}");
        if (bindable is LoadingOverlay overlay && newValue is bool isVisible)
        {
            // Ensure UI updates happen on main thread
            MainThread.BeginInvokeOnMainThread(() =>
            {
                Debug.WriteLine($"[LoadingOverlay] OnIsVisibleChanged - Setting overlayGrid.IsVisible to: {isVisible}");
                // Update the grid visibility
                overlay.overlayGrid.IsVisible = isVisible;
                
                if (isVisible)
                {
                    Debug.WriteLine("[LoadingOverlay] Starting animations");
                    overlay.StartAnimations();
                }
                else
                {
                    Debug.WriteLine("[LoadingOverlay] Stopping animations");
                    overlay.StopAnimations();
                }
            });
        }
    }

    private async void StartAnimations()
    {
        _animationCancellation?.Cancel();
        _animationCancellation = new CancellationTokenSource();
        var token = _animationCancellation.Token;

        // Fade in animation for overlay
        overlayGrid.Opacity = 0;
        await overlayGrid.FadeTo(1, 300);

        // Scale animation for frame
        loadingFrame.Scale = 0.8;
        loadingFrame.Opacity = 0;
        await Task.WhenAll(
            loadingFrame.FadeTo(1, 200),
            loadingFrame.ScaleTo(1, 300, Easing.SpringOut)
        );

        // Start continuous animations
        if (!token.IsCancellationRequested)
        {
            _ = RotateCircleAsync(token);
            _ = PulseDotsAsync(token);
            _ = AnimateLoadingTextAsync(token);
        }
    }

    private async Task RotateCircleAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested && IsVisible)
        {
            await rotatingCircle.RotateTo(360, 2000, Easing.Linear);
            if (!token.IsCancellationRequested)
            {
                rotatingCircle.Rotation = 0;
            }
        }
    }

    private async Task PulseDotsAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested && IsVisible)
        {
            // Animate dots with delay
            var tasks = new[]
            {
                PulseDotAsync(dot1, 0, token),
                PulseDotAsync(dot2, 200, token),
                PulseDotAsync(dot3, 400, token)
            };
            
            await Task.WhenAll(tasks);
            
            if (!token.IsCancellationRequested && IsVisible)
            {
                await Task.Delay(300, token);
            }
        }
    }

    private async Task PulseDotAsync(BoxView dot, uint delay, CancellationToken token)
    {
        if (delay > 0)
        {
            await Task.Delay((int)delay, token);
        }
        
        if (!token.IsCancellationRequested)
        {
            await dot.FadeTo(1, 300, Easing.SinInOut);
            if (!token.IsCancellationRequested)
            {
                await dot.FadeTo(0.3, 300, Easing.SinInOut);
            }
        }
    }

    private async Task AnimateLoadingTextAsync(CancellationToken token)
    {
        var messages = new[] { "Loading...", "Authenticating...", "Please wait..." };
        int index = 0;
        
        while (!token.IsCancellationRequested && IsVisible)
        {
            loadingLabel.Text = messages[index];
            await loadingLabel.FadeTo(0.5, 500, Easing.SinInOut);
            if (!token.IsCancellationRequested)
            {
                await loadingLabel.FadeTo(1, 500, Easing.SinInOut);
            }
            index = (index + 1) % messages.Length;
            await Task.Delay(1000, token);
        }
    }

    private async void StopAnimations()
    {
        _animationCancellation?.Cancel();
        
        // Fade out animation
        await overlayGrid.FadeTo(0, 200);
    }
}


