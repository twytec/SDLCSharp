using System;

namespace SDLCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (SDL2.SDL.SDL_Init(SDL2.SDL.SDL_INIT_EVERYTHING) != 0)
            {
                Console.WriteLine($"SDL_Init Error: {SDL2.SDL.SDL_GetError()}");
                return;
            }

            IntPtr win = SDL2.SDL.SDL_CreateWindow("Hello World!", 100, 100, 620, 387, SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);
            if (win == IntPtr.Zero)
            {
                Console.WriteLine($"SDL_CreateWindow Error: {SDL2.SDL.SDL_GetError()}");
                return;
            }

            IntPtr ren = SDL2.SDL.SDL_CreateRenderer(win, -1, SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL2.SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            if (ren == IntPtr.Zero)
            {
                Console.WriteLine($"SDL_CreateRenderer Error: {SDL2.SDL.SDL_GetError()}");
                if (win != IntPtr.Zero)
                {
                    SDL2.SDL.SDL_DestroyWindow(win);
                }
                SDL2.SDL.SDL_Quit();
                return;
            }

            IntPtr bmp = SDL2.SDL.SDL_LoadBMP(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "grumpy-cat.bmp"));
            if (bmp == IntPtr.Zero)
            {
                Console.WriteLine($"SDL_LoadBMP Error: {SDL2.SDL.SDL_GetError()}");
                if (ren != IntPtr.Zero)
                {
                    SDL2.SDL.SDL_DestroyRenderer(ren);
                }
                if (win != IntPtr.Zero)
                {
                    SDL2.SDL.SDL_DestroyWindow(win);
                }
                SDL2.SDL.SDL_Quit();
                return;
            }

            IntPtr tex = SDL2.SDL.SDL_CreateTextureFromSurface(ren, bmp);
            if (tex == IntPtr.Zero)
            {
                Console.WriteLine($"SDL_CreateTextureFromSurface Error: {SDL2.SDL.SDL_GetError()}");
                if (bmp != IntPtr.Zero)
                {
                    SDL2.SDL.SDL_FreeSurface(bmp);
                }
                if (ren != IntPtr.Zero)
                {
                    SDL2.SDL.SDL_DestroyRenderer(ren);
                }
                if (win != IntPtr.Zero)
                {
                    SDL2.SDL.SDL_DestroyWindow(win);
                }
                SDL2.SDL.SDL_Quit();
                return;
            }
            SDL2.SDL.SDL_FreeSurface(bmp);

            for (int i = 0; i < 20; i++)
            {
                SDL2.SDL.SDL_RenderClear(ren);
                SDL2.SDL.SDL_RenderCopy(ren, tex, IntPtr.Zero, IntPtr.Zero);
                SDL2.SDL.SDL_RenderPresent(ren);
                SDL2.SDL.SDL_Delay(100);
            }

            SDL2.SDL.SDL_DestroyTexture(tex);
            SDL2.SDL.SDL_DestroyRenderer(ren);
            SDL2.SDL.SDL_DestroyWindow(win);
            SDL2.SDL.SDL_Quit();
        }
    }
}
