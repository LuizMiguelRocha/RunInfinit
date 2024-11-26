﻿namespace Corrida;

using FFImageLoading.Maui;

public partial class MainPage : ContentPage
{

		public MainPage()
	{
		InitializeComponent();
		player = new Player(imgPlayer);
		player.Run();
	}

	int count = 0;
	bool EstaMorto = false;
	bool EstaPulando = false;
	const int TempoEntreFrames = 25;
	int Velocidade1 = 0;
	int Velocidade2 = 0;
	int Velocidade3 = 0;
	int Velocidade = 0;
	int LarguraJanela = 0;
	int AlturaJanela = 0;
	const int ForcaGravidade = 6;
	bool EstaNoChao = true;
	bool EstaNoAr = false;
	int TempoPulando = 0;
	int TempoNoAr = 0;
	const int ForcaPulo = 8;
	const int MaxTempoPulando = 6;
	const int MaxTempoNoAr = 4;
	Player player;

	protected override void OnSizeAllocated(Double w, Double h)
	{
		base.OnSizeAllocated(w,h);
		CorrigeTamanhoCenario(w,h);
		CalculaVelocidade(w);
	}

	void CalculaVelocidade(Double w)
	{
		Velocidade1 = (int)(w * 0.001);
		Velocidade2 = (int)(w * 0.004);
		Velocidade3 = (int)(w * 0.008);
		Velocidade = (int)(w * 0.01);
	}

	void CorrigeTamanhoCenario(Double w, Double h)
	{
		foreach (var a in HSLayer1.Children)
		(a as Image).WidthRequest = w;
		foreach (var a in HSLayerChao.Children)
		(a as Image).WidthRequest = w;

		HSLayer1.WidthRequest = w * 2.0;
		HSLayerChao.WidthRequest = w * 1.5;
	}

	void GerenciaCenarios()
	{
		MoveCenario();
		GerenciaCenario(HSLayer1);
		GerenciaCenario(HSLayerChao);

	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}

	async Task Desenha()
	{
		while(!EstaMorto)
		{
			GerenciaCenarios();
			player.Desenha();
			await Task.Delay(TempoEntreFrames);
		}
		if(!EstaPulando && !EstaNoAr)
		{
			AplicaGravidade();
			player.Desenha();
		}
		else
		AplicaPulo();
		await Task.Delay(TempoEntreFrames);
	}

	void MoveCenario()
	{
		HSLayer1.TranslationX -= Velocidade1;
		HSLayerChao.TranslationX -= Velocidade;
	}

	void GerenciaCenario(HorizontalStackLayout HSL)
	{
		var view = (HSL.Children.First() as Image);
		if (view.WidthRequest + HSL.TranslationX < 0)
		{
			HSL.Children.Remove(view);
			HSL.Children.Add(view);
			HSL.TranslationX = view.TranslationX;
		}
	}

	void AplicaGravidade()
	{
		if (player.GetY() < 0)
		player.MoveY(ForcaGravidade);
		else if (player.GetY() > 0)
		{
			player.SetY(0);
			EstaNoChao = true;
		}
	}
	
	void AplicaPulo()
	{
		EstaNoChao = false;
		if(EstaPulando && TempoPulando >= MaxTempoPulando)
		{
			EstaPulando = false;
			EstaNoAr = false;
			TempoNoAr = 0;
		}
		else if (EstaNoAr && TempoNoAr >= MaxTempoNoAr)
		{
			EstaPulando = false;
			EstaNoAr = false;
			TempoPulando = 0;
			TempoNoAr = 0;
		}
		else if (EstaPulando && TempoPulando < MaxTempoPulando)
		{
			player.MoveY (-ForcaPulo);
			TempoPulando++;
		}
		else if (EstaNoAr)
		{
			TempoNoAr++;
		}
	}
}