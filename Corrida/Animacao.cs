namespace Corrida;

using FFImageLoading.Maui;
	public class Animacao
	{
		protected List<String> Animacao1 = new List<String>();
		protected List<String> Animacao2 = new List<String>();
		protected List<String> Animacao3 = new List<String>();
		protected bool Loop = true;
		protected int AnimacaoAtiva = 1;
		bool Parado = true;
		int FrameAtual = 1;
		protected CachedImageView imageView;


		public Animacao(CachedImageView a)
		{
			imageView = a;
		}

		public void Stop()
	{
		Parado = true;
	}

	public void Play()
	{
		Parado = false;
	}

	public void SetAnimacaoAtiva(int a)
	{
		AnimacaoAtiva = a;
	}

	public void Desenha()
	{
		if(Parado)
		return;
		string foxy01 = "foxy01";
		int TamanhoAnimacao = 2;
		if(AnimacaoAtiva == 1)
		{
			foxy01 = Animacao1 [FrameAtual];
			TamanhoAnimacao = Animacao1.Count;
		} 
		else if (AnimacaoAtiva == 2)
		{
			foxy01 = Animacao2[FrameAtual];
			TamanhoAnimacao = Animacao2.Count;
		}

		imageView.Source = ImageSource.FromFile(foxy01);
		FrameAtual++;

		if (FrameAtual >= TamanhoAnimacao)
		{
			if (Loop)
			FrameAtual = 0;
			else
			{
				Parado = true;
				QuandoParar();
			}
		}
	}
	public virtual void QuandoParar()
	{

	}

	 public void MoveY(int s)
    {
        imageView.TranslationY += s;
    }

    public double GetY()
    {
        return imageView.TranslationY;
    }

    public void SetY(double a)
    {
        imageView.TranslationY = a;
    }

    }  