	interface contractFinancialTransactionDTO {
		id: any;
		contractNumber: string;
		contractId: any;
		amount: number;
		paidAmount: number;
		balance: number;
		contractAdditionId?: any;
		dueDate: Date;
		isPaid: boolean;
		isCancelled: boolean;
		contractAddition: string;
		buildingId: any;
		unitId: any;
	}
