import { DatePipe } from "@angular/common";
import { Component, OnInit, ViewChild } from "@angular/core";
import {
    AbstractControl,
    FormArray,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { v4 as uuidv4 } from "uuid";
import { ActivatedRoute, Router } from "@angular/router";
import { NgSelectComponent } from "@ng-select/ng-select";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Building } from "../../../Models/building";
import { ContractFinancialTransaction } from "../../../Models/contract-financial-transaction";
import { Renter } from "../../../Models/renter";
import { Unit } from "../../../Models/unit";
import { Sanad } from "../../../Models/sanad";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { BuildingService } from "../../../Services/building.service";
import { RenterContractService } from "../../../Services/renter-contract.service";
import { RenterService } from "../../../Services/renter.service";
import { UnitService } from "../../../Services/unit.service";
import { SanadDetails } from "../../../Models/sanad-details";
import { VoucherService } from "../../../Services/voucher.service";
import { TranslationService } from "../../../Services/translation.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { DialogService } from "../../../Services/dialog.service";
import { Enum } from "../../../Models/enum";
import { CashBankService } from "../../../Services/cash-bank.service";
import { CashBank } from "../../../Models/cash-bank";
import { Owner } from "../../../Models/owner";
import { Contributer } from "../../../Models/contributer";
import { Expense } from "../../../Models/expense";
import { OwnerService } from "../../../Services/owner.service";
import { ContributerService } from "../../../Services/contributer.service";
import { ExpenseService } from "../../../Services/expense.service";
import { forkJoin } from "rxjs";

@Component({
    selector: "app-receipt-details",
    templateUrl: "./receipt-details.component.html",
    styleUrls: ["./receipt-details.component.css"],
})
export class ReceiptDetailsComponent implements OnInit {
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    voucher: Sanad = { sanadTypeId: 1, sanadDetails: [] };
    voucherForm: FormGroup = this.fb.group({});
    breadcrumbItems: Breadcrumb[] = [];
    cashOrBanks: CashBank[] = [];
    buildings: Building[] = [];
    units: Unit[] = [];
    owners: Owner[] = [];
    renters: Renter[] = [];
    contributers: Contributer[] = [];
    expenses: Expense[] = [];
    submitted = false;
    showInstallmentBtn = false;
    installments: ContractFinancialTransaction[] = [];
    sanadTypes: Enum[] = [];
    showForm = false;
    showPostButton = true;
    constructor(
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private cashBankService: CashBankService,
        private buildingService: BuildingService,
        private unitService: UnitService,
        private ownerService: OwnerService,
        private router: Router,
        private renterContractService: RenterContractService,
        private voucherService: VoucherService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private dialogService: DialogService,
        private renterService: RenterService,
        private contributerService: ContributerService,
        private expenseService: ExpenseService,
        private route: ActivatedRoute
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.ReceiptDetailsItems;
        this.sanadTypes = this.voucherService.SanadTypes;

        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.loadDataWithVoucher(id);
        } else {
            this.loadData();
        }
    }

    loadDataWithVoucher(voucherId: any) {
        forkJoin(
            this.cashBankService.GetAll(),
            this.buildingService.GetAllBuildings(),
            this.unitService.GetAll(),
            this.ownerService.GetAllOwners(),
            this.renterService.GetAll(),
            this.contributerService.GetAll(),
            this.expenseService.GetAll(),
            this.voucherService.GetReceiptById(voucherId)
        ).subscribe(
            ([
                cashRes,
                buildingRes,
                unitRes,
                ownerRes,
                renterRes,
                contributerRes,
                expenseRes,
                voucherRes,
            ]) => {
                this.cashOrBanks = cashRes;
                this.buildings = buildingRes;
                this.units = unitRes;
                this.owners = ownerRes;
                this.renters = renterRes;
                this.contributers = contributerRes;
                this.expenses = expenseRes;
                this.voucher = voucherRes;
                this.showInstallmentBtn = this.voucher.renterId != null;

                if (this.showInstallmentBtn) {
                    this.renterContractService
                        .GetInstallments(this.voucher.renterId)
                        .subscribe(
                            (res) => {
                                this.installments = res;
                                for (const installment of this.installments) {
                                    const index =
                                        this.voucher.sanadDetails?.findIndex(
                                            (x) =>
                                                x.installmentId ==
                                                installment.id
                                        );
                                    installment.selected = index != -1;
                                }
                            },
                            (error) => console.log(error)
                        );
                }

                const lines = [];
                for (const detail of this.voucher.sanadDetails) {
                    lines.push(
                        this.fb.group({
                            ExpenseId: [
                                detail.expenseId,
                                this.requiredFieldWhenAnotherHasValue1(
                                    this.voucherForm,
                                    "SandadTypeId",
                                    "1"
                                ),
                            ],
                            InstallmentId: [detail.installmentId],
                            Installment: [detail.installment],
                            DueDate: [
                                new DatePipe("en-US").transform(
                                    detail.dueDate,
                                    "yyyy-MM-dd"
                                ),
                            ],
                            BuildingId: [detail.buildingId],
                            UnitId: [detail.unitId],
                            Amount: [detail.amount, [Validators.required]],
                        })
                    );
                }

                this.createForm(
                    this.voucher.autoNumber,
                    this.voucher.sanadTypeId,
                    this.voucher.sanadNumber,
                    this.voucher.from,
                    this.voucher.sanadDate,
                    this.voucher.amount,
                    this.voucher.description,
                    this.voucher.cashBankId,
                    this.voucher.ownerId,
                    this.voucher.renterId,
                    this.voucher.contributerId,
                    lines
                );
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }
    loadData() {
        forkJoin(
            this.cashBankService.GetAll(),
            this.buildingService.GetAllBuildings(),
            this.unitService.GetAll(),
            this.ownerService.GetAllOwners(),
            this.renterService.GetAll(),
            this.contributerService.GetAll(),
            this.expenseService.GetAll()
        ).subscribe(
            ([
                cashRes,
                buildingRes,
                unitRes,
                ownerRes,
                renterRes,
                contributerRes,
                expenseRes,
            ]) => {
                this.cashOrBanks = cashRes;
                this.buildings = buildingRes;
                this.units = unitRes;
                this.owners = ownerRes;
                this.renters = renterRes;
                this.contributers = contributerRes;
                this.expenses = expenseRes;
                this.createForm(
                    null,
                    1,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    []
                );
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }

    createForm(
        autoNumber: any,
        sanadTypeId: any,
        sanadNumber: any,
        from: any,
        sanadDate: any,
        amount: any,
        description: any,
        cashBankId: any,
        ownerId: any,
        renterId: any,
        contributerId: any,
        installments: FormGroup[]
    ) {
        this.voucherForm = this.fb.group(
            {
                AutoNumber: [autoNumber],
                SandadTypeId: [sanadTypeId, Validators.required],
                SanadNumber: [sanadNumber],
                From: [from],
                SanadDate: [sanadDate, Validators.required],
                Amount: [
                    amount,
                    this.requiredFieldWhenAnotherHasValue("SandadTypeId", "4"),
                ],
                Description: [description, Validators.required],
                CashBankId: [cashBankId, [Validators.required]],
                OwnerId: [
                    ownerId,
                    this.requiredFieldWhenAnotherHasValue("SandadTypeId", "3"),
                ],
                RenterId: [
                    renterId,
                    this.requiredFieldWhenAnotherHasValue("SandadTypeId", "2"),
                ],
                ContributerId: [
                    contributerId,
                    this.requiredFieldWhenAnotherHasValue("SandadTypeId", "4"),
                ],
                Installments: this.fb.array(installments),
            },
            { validators: [this.validateFormAmount] }
        );
        this.showForm = true;
        this.voucherForm?.valueChanges?.subscribe(() => {
            if (this.voucherForm?.dirty) {
                this.showPostButton = false;
            }
        });
        this.Installments?.valueChanges?.subscribe(() => {
            this.showPostButton = false;
        });
    }
    get SandadTypeId() {
        return this.voucherForm.controls["SandadTypeId"] as FormControl;
    }
    get SanadDate() {
        return this.voucherForm.controls["SanadDate"] as FormControl;
    }
    get Amount() {
        return this.voucherForm.controls["Amount"] as FormControl;
    }
    get Description() {
        return this.voucherForm.controls["Description"] as FormControl;
    }
    get CashBankId() {
        return this.voucherForm.controls["CashBankId"] as FormControl;
    }
    get RenterId() {
        return this.voucherForm.controls["RenterId"] as FormControl;
    }
    get OwnerId() {
        return this.voucherForm.controls["OwnerId"] as FormControl;
    }
    get ContributerId() {
        return this.voucherForm.controls["ContributerId"] as FormControl;
    }
    get Installments() {
        return this.voucherForm.controls["Installments"] as FormArray;
    }
    get InstallmentsFormGroups() {
        const ctrs = this.Installments.controls as FormGroup[];
        let amount = 0;
        for (const ctr of ctrs || []) {
            const val = ctr.controls["Amount"]?.value || 0;
            amount += +val;
        }
        this.Amount.setValue(amount == 0 ? null : amount);
        return ctrs;
    }

    loadInstallments(renterId: number) {
        this.renterContractService.GetInstallments(renterId).subscribe(
            (res) => {
                this.installments = res;
                for (const installment of this.installments) {
                    installment.selected = false;
                }
            },
            (error) => console.log(error)
        );
    }

    backToList() {
        this.router.navigate(["/financials/receipts"]);
    }

    onSubmit() {
        this.submitted = true;
        if (this.voucherForm.invalid) {
            return;
        }
        if (this.Amount.value <= 0) {
            this.voucherForm.setErrors({ nolines: true });
            return;
        }
        const formValue = this.voucherForm.value;
        const voucherDetails: SanadDetails[] = [];
        for (const inst of formValue.Installments) {
            voucherDetails.push({
                id: uuidv4(),
                expenseId: inst.ExpenseId,
                buildingId: inst.BuildingId,
                unitId: inst.UnitId,
                amount: inst.Amount,
                installmentId: inst.InstallmentId,
                dueDate: inst.DueDate,
                installment: inst.Installment,
            });
        }
        this.voucher.sanadDetails = voucherDetails;
        if (this.voucher.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }
    Add() {
        this.voucher.id = uuidv4();
        this.voucherService.CreateReceipt(this.voucher).subscribe(
            (_) => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => {
                this.voucher.id = null;
                this.submitted = false;
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }
    Update() {
        this.voucherService
            .UpdateReceipt(this.voucher.id, this.voucher)
            .subscribe(
                (_) => {
                    const successMsg = this.translateService.Translate(
                        "UpdatedSuccessfully"
                    );
                    this.alertify.success(successMsg);
                    this.backToList();
                },
                (error) => {
                    this.submitted = false;
                    const msg =
                        this.translateService.Translate("ErrorOccurred");
                    this.alertify.error(msg);
                    console.log(error);
                }
            );
    }
    addInstallments() {
        this.modal?.showModal();
    }
    addNewInstallment(
        expenseId: any,
        installmentId: any,
        installment: any,
        dueDate: any,
        buildingId: any,
        unitId: any,
        amount: any
    ) {
        this.Installments.push(
            this.fb.group({
                ExpenseId: [
                    expenseId,
                    this.requiredFieldWhenAnotherHasValue1(
                        this.voucherForm,
                        "SandadTypeId",
                        "1"
                    ),
                ],
                InstallmentId: [installmentId],
                Installment: [installment],
                DueDate: [
                    new DatePipe("en-US").transform(dueDate, "yyyy-MM-dd"),
                ],
                BuildingId: [buildingId],
                UnitId: [unitId],
                Amount: [amount, [Validators.required]],
            })
        );
    }
    deleteRecord(index: number, installmentId?: any) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.Installments.removeAt(index);
                if (installmentId) {
                    this.installments.forEach((x) => {
                        if (x.id == installmentId) {
                            x.selected = false;
                        }
                    });
                }
            }
        });
    }

    PostVoucher(id: any) {
        const Msg = this.translateService.Translate("PostMessage");
        this.dialogService.confirm(Msg).subscribe((res) => {
            if (res) {
                this.voucherService.PostReceipt(id).subscribe(
                    (_) => {
                        const successMsg =
                            this.translateService.Translate(
                                "PostedSuccessfully"
                            );
                        this.alertify.success(successMsg);
                        this.backToList();
                    },
                    (error) => {
                        const msg =
                            this.translateService.Translate("ErrorOccurred");
                        this.alertify.error(msg);
                        console.log(error);
                    }
                );
            }
        });
    }

    HideModal() {
        this.modal?.hideModal();
    }
    addSelectedInstallments(installments: ContractFinancialTransaction[]) {
        this.Installments.clear();
        for (const installment of installments) {
            this.addNewInstallment(
                null,
                installment.id,
                installment.balance,
                installment.dueDate,
                installment.buildingId,
                installment.unitId,
                installment.balance
            );
        }
        this.HideModal();
    }
    getControlByName(fg: FormGroup, name: string) {
        return fg.controls[name] as FormControl;
    }
    renterChange(renter: NgSelectComponent) {
        this.showInstallmentBtn = renter.hasValue;
        this.Installments.clear();
        if (renter.hasValue) {
            this.loadInstallments(renter.selectedValues[0]);
        } else {
            this.installments = [];
        }
    }
    validateDropdown(control: AbstractControl) {
        const thisValue = control.value;
        if (
            thisValue == undefined ||
            thisValue == null ||
            thisValue == "" ||
            thisValue == "null"
        ) {
            return { required: true };
        }
        return null;
    }
    keyPressNumbers(event: any) {
        const charCode = event.which ? event.which : event.keyCode;
        // Only Numbers 0-9
        if (charCode < 48 || charCode > 57) {
            event.preventDefault();
            return false;
        } else {
            return true;
        }
    }
    keyPressNumbersWithDecimal(event: any, value: any) {
        const charCode = event.which ? event.which : event.keyCode;
        if (charCode === 46 && value?.indexOf(event.key) > -1) {
            return false;
        }
        if (
            charCode !== 46 &&
            charCode > 31 &&
            (charCode < 48 || charCode > 57)
        ) {
            event.preventDefault();
            return false;
        }
        return true;
    }
    requiredFieldWhenAnotherHasValue(otherControlName: string, value: any) {
        return (
            control: AbstractControl
        ): { [key: string]: boolean } | null => {
            if (!control.parent) {
                return null;
            }
            const otherValue = control?.parent?.get(otherControlName)?.value;
            const thisValue = control.value;
            if (
                otherValue == value &&
                (thisValue == undefined ||
                    thisValue == null ||
                    thisValue == 0 ||
                    thisValue == "" ||
                    thisValue == "0")
            ) {
                return {
                    required: true,
                };
            }
            return null;
        };
    }

    requiredFieldWhenAnotherHasValue1(
        form: FormGroup,
        otherControlName: string,
        value: any
    ) {
        return (
            control: AbstractControl
        ): { [key: string]: boolean } | null => {
            const otherValue = form.get(otherControlName)?.value;
            const thisValue = control.value;
            if (
                otherValue == value &&
                (thisValue == undefined ||
                    thisValue == null ||
                    thisValue == 0 ||
                    thisValue == "" ||
                    thisValue == "0")
            ) {
                return {
                    required: true,
                };
            }
            return null;
        };
    }
    typeChange() {
        this.voucher.ownerId = null;
        this.voucher.renterId = null;
        this.voucher.contributerId = null;
        this.showInstallmentBtn = false;
        this.Installments.clear();
        if (this.voucher.sanadTypeId == 4) {
            this.Amount.reset();
        }
    }

    onUnitChange(unitCTR: NgSelectComponent, buildingCTR: NgSelectComponent) {
        if (unitCTR.hasValue) {
            const unitId = unitCTR.selectedValues[0];
            const buildingId = this.units.find(
                (x) => x.id == unitId
            )?.buildingId;
            buildingCTR.writeValue(buildingId);
        }
    }

    onBuildingChange(
        unitCTR: NgSelectComponent,
        buildingCTR: NgSelectComponent
    ) {
        if (buildingCTR.hasValue) {
            if (unitCTR.hasValue) {
                const buildingId = buildingCTR.selectedValues[0];
                const unitId = unitCTR.selectedValues[0];
                const selectedUnit = this.units.find((x) => x.id == unitId);
                if (selectedUnit && selectedUnit.buildingId != buildingId) {
                    unitCTR.writeValue(null);
                }
            }
        }
    }

    validateFormAmount(form: FormGroup) {
        if (form.value.Amount <= 0) {
            return { nolines: true };
        }
        return null;
    }
}
