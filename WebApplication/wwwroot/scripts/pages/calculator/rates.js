const rates = {
	small: {
		isFixed: true,
		prices: [
			{
				maxWeight: 1,
				maxVolume: 0.005,
				values: {
					'Moscow-Lipetsk': 200,
					'Lipetsk-Moscow': 100
				}
			},
			{
				maxWeight: 5,
				maxVolume: 0.025,
				values: {
					'Moscow-Lipetsk': 250,
					'Lipetsk-Moscow': 150
				}
			},
			{
				maxWeight: 10,
				maxVolume: 0.05,
				values: {
					'Moscow-Lipetsk': 300,
					'Lipetsk-Moscow': 200
				}
			},
			{
				maxWeight: 20,
				maxVolume: 0.1,
				values: {
					'Moscow-Lipetsk': 300,
					'Lipetsk-Moscow': 230
				}
			}
		]
	},
	default: {
		isFixed: true,
		prices: [
			{
				maxWeight: 110 + 1,
				maxVolume: 0.5,
				values: {
					'Moscow-Lipetsk': 1380,
					'Lipetsk-Moscow': 1150
				}
			},
			{
				maxWeight: 330,
				maxVolume: 1.5,
				values: {
					'Moscow-Lipetsk': 1334,
					'Lipetsk-Moscow': 1150
				}
			},
			{
				maxWeight: 880,
				maxVolume: 4.0,
				values: {
					'Moscow-Lipetsk': 1288,
					'Lipetsk-Moscow': 1058
				}
			},
			{
				maxWeight: 1650,
				maxVolume: 7.5,
				values: {
					'Moscow-Lipetsk': 1265,
					'Lipetsk-Moscow': 1012
				}
			},
			{
				maxWeight: 3300,
				maxVolume: 15.0,
				values: {
					'Moscow-Lipetsk': 1242,
					'Lipetsk-Moscow': 966
				}
			},
			{
				maxWeight: 4400,
				maxVolume: 20.0,
				values: {
					'Moscow-Lipetsk': 1219,
					'Lipetsk-Moscow': 920
				}
			}
		]
	},
	weight: {
		isFixed: false,
		prices: [
			{
				maxWeight: 100,
				values: {
					'Moscow-Lipetsk': 6.0,
					'Lipetsk-Moscow': 5.0
				}
			},
			{
				maxWeight: 300,
				values: {
					'Moscow-Lipetsk': 5.8,
					'Lipetsk-Moscow': 5.0
				}
			},
			{
				maxWeight: 800,
				values: {
					'Moscow-Lipetsk': 5.6,
					'Lipetsk-Moscow': 4.6
				}
			},
			{
				maxWeight: 1500,
				values: {
					'Moscow-Lipetsk': 5.5,
					'Lipetsk-Moscow': 4.4
				}
			},
			{
				maxWeight: 3000,
				values: {
					'Moscow-Lipetsk': 5.4,
					'Lipetsk-Moscow': 4.2
				}
			},
			{
				maxWeight: 5000,
				values: {
					'Moscow-Lipetsk': 5.3,
					'Lipetsk-Moscow': 4.0
				}
			}
		]
	}
};