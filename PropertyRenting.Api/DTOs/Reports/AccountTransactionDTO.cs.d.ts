	interface accountTransactionDTO {
		type: number;
		debitAmount?: number;
		creditAmount?: number;
		autoNumber?: number;
		voucherId: string;
		voucherDate?: Date;
		referenceId?: any;
		referenceType: string;
		referenceAutoNumber?: number;
		referenceManualNumber: string;
		description: string;
	}
