using Zin.Editor.Buffer;
using Zin.Platform;

namespace Zin.Editor.Rendering;

public sealed class DefaultOutputPipe : IOutputPipe
{
    public void RenderLine(ITerminal terminal, RenderChain renderChain, string line, int offset, int length)
    {
        renderChain.Write(line, offset, length);
    }

    public void RenderLine(ITerminal terminal, RenderChain renderChain, GapBuffer line, int offset, int length)
    {
        renderChain.Write(line.ToString(), offset, length);
    }
}