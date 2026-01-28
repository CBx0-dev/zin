using Zin.Editor.Buffer;
using Zin.Platform;

namespace Zin.Editor.Rendering;

public interface IOutputPipe
{
    public void RenderLine(ITerminal terminal, RenderChain renderChain, string line, int offset, int length);

    public void RenderLine(ITerminal terminal, RenderChain renderChain, GapBuffer line, int offset, int length);
}