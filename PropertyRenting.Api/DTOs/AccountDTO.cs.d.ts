	interface accountDTO {
		id: any;
		nameAR: string;
		nameEN: string;
		accountTypeId: number;
		isCashOrBank: boolean;
		code: string;
		parentId?: any;
		level: number;
		accountChildren: server.accountDTO[];
	}
