import { Component, OnInit } from "@angular/core";
import { v4 as uuidv4 } from "uuid";
import {
    AbstractControl,
    FormArray,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ActivatedRoute, Router } from "@angular/router";
import { forkJoin } from "rxjs";
import { Account } from "../../../Models/account";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { Voucher } from "../../../Models/voucher";
import { VoucherDetails } from "../../../Models/voucher-details";
import { AccountService } from "../../../Services/account.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { DialogService } from "../../../Services/dialog.service";
import { TranslationService } from "../../../Services/translation.service";
import { VoucherService } from "../../../Services/voucher.service";
import { Building } from "../../../Models/building";
import { Unit } from "../../../Models/unit";
import { Owner } from "../../../Models/owner";
import { Renter } from "../../../Models/renter";
import { Contributer } from "../../../Models/contributer";
import { BuildingService } from "../../../Services/building.service";
import { UnitService } from "../../../Services/unit.service";
import { ContributerService } from "../../../Services/contributer.service";
import { RenterService } from "../../../Services/renter.service";
import { OwnerService } from "../../../Services/owner.service";
import { NgSelectComponent } from "@ng-select/ng-select";
import { CashBank } from "../../../Models/cash-bank";
import { CashBankService } from "../../../Services/cash-bank.service";

@Component({
    selector: "app-voucher-details",
    templateUrl: "./voucher-details.component.html",
    styleUrls: ["./voucher-details.component.css"],
})
export class VoucherDetailsComponent implements OnInit {
    voucher: Voucher = { voucherDetails: [] };
    voucherForm!: FormGroup;
    breadcrumbItems: Breadcrumb[] = [];
    accounts: Account[] = [];
    buildings: Building[] = [];
    units: Unit[] = [];
    owners: Owner[] = [];
    renters: Renter[] = [];
    contributers: Contributer[] = [];
    cashBanks: CashBank[] = [];
    submitted = false;
    showForm = false;
    showPostButton = true;
    constructor(
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private router: Router,
        private route: ActivatedRoute,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private dialogService: DialogService,
        private voucherService: VoucherService,
        private accountService: AccountService,
        private buildingService: BuildingService,
        private unitService: UnitService,
        private ownerService: OwnerService,
        private renterService: RenterService,
        private contributerService: ContributerService,
        private cashBankService: CashBankService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.VoucherDetailsItems;

        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.loadDataWithVoucher(id);
        } else {
            this.loadData();
        }
    }
    loadDataWithVoucher(voucherId: any) {
        forkJoin(
            this.accountService.GetAll(),
            this.buildingService.GetAllBuildings(),
            this.unitService.GetAll(),
            this.ownerService.GetAllOwners(),
            this.renterService.GetAll(),
            this.contributerService.GetAll(),
            this.cashBankService.GetAll(),
            this.voucherService.GetVoucherById(voucherId)
        ).subscribe(
            ([
                accountRes,
                buildingRes,
                unitRes,
                ownerRes,
                renterRes,
                contributerRes,
                cashBankRes,
                voucherRes,
            ]) => {
                this.accounts = accountRes.filter(
                    (x) => (x.accountTypeId || 0) != 5
                );
                this.buildings = buildingRes;
                this.units = unitRes;
                this.owners = ownerRes;
                this.renters = renterRes;
                this.contributers = contributerRes;
                this.cashBanks = cashBankRes;
                this.voucher = voucherRes;

                const lines = [];
                for (const detail of this.voucher.voucherDetails) {
                    lines.push(
                        this.fb.group({
                            AccountId: [detail.accountId, Validators.required],
                            DebitAmount: [
                                detail.debitAmount,
                                this.invalidValidation("CreditAmount"),
                            ],
                            CreditAmount: [
                                detail.creditAmount,
                                this.invalidValidation("DebitAmount"),
                            ],
                            RenterId: [detail.renterId],
                            OwnerId: [detail.ownerId],
                            ContributerId: [detail.contributerId],
                            BuildingId: [detail.buildingId],
                            UnitId: [detail.unitId],
                            CashBankId: [detail.cashBankId],
                        })
                    );
                }
                this.createForm(
                    this.voucher.autoNumber,
                    this.voucher.voucherId,
                    this.voucher.voucherDate,
                    this.voucher.description,
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
            this.accountService.GetAll(),
            this.buildingService.GetAllBuildings(),
            this.unitService.GetAll(),
            this.ownerService.GetAllOwners(),
            this.renterService.GetAll(),
            this.contributerService.GetAll(),
            this.cashBankService.GetAll()
        ).subscribe(
            ([
                accountRes,
                buildingRes,
                unitRes,
                ownerRes,
                renterRes,
                contributerRes,
                cashBankRes,
            ]) => {
                this.accounts = accountRes.filter(
                    (x) => (x.accountTypeId || 0) != 5
                );
                this.buildings = buildingRes;
                this.units = unitRes;
                this.owners = ownerRes;
                this.renters = renterRes;
                this.contributers = contributerRes;
                this.cashBanks = cashBankRes;
                this.createForm(null, null, null, null, []);
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
        voucherNumber: any,
        voucherDate: any,
        description: any,
        voucherLines: FormGroup[]
    ) {
        this.voucherForm = this.fb.group({
            AutoNumber: [autoNumber],
            VoucherNumber: [voucherNumber, Validators.required],
            VoucherDate: [voucherDate, Validators.required],
            Description: [description, Validators.required],
            VoucherLines: this.fb.array(voucherLines),
        });

        this.showForm = true;
        this.voucherForm?.valueChanges?.subscribe(() => {
            if (this.voucherForm?.dirty) {
                this.showPostButton = false;
            }
        });
        this.VoucherLines?.valueChanges?.subscribe(() => {
            this.showPostButton = false;
        });
    }

    get VoucherNumber() {
        return this.voucherForm.controls["VoucherNumber"] as FormControl;
    }
    get VoucherDate() {
        return this.voucherForm.controls["VoucherDate"] as FormControl;
    }
    get Description() {
        return this.voucherForm.controls["Description"] as FormControl;
    }
    get VoucherLines() {
        return this.voucherForm.controls["VoucherLines"] as FormArray;
    }
    get VoucherLinesFormGroups() {
        return this.VoucherLines.controls as FormGroup[];
    }

    backToList() {
        this.router.navigate(["/financials/vouchers"]);
    }
    onSubmit() {
        this.submitted = true;
        if (this.voucherForm.invalid) {
            return;
        }
        let totalDebit = 0;
        let totalCredit = 0;

        const formValue = this.voucherForm.value;
        const voucherDetails: VoucherDetails[] = [];

        if (this.VoucherLines.length <= 0) {
            this.voucherForm.setErrors({ nolines: true });
            return;
        }
        for (const inst of formValue.VoucherLines) {
            voucherDetails.push({
                id: uuidv4(),
                accountId: inst.AccountId,
                creditAmount: inst.CreditAmount,
                debitAmount: inst.DebitAmount,
                renterId: inst.RenterId,
                ownerId: inst.OwnerId,
                contributerId: inst.ContributerId,
                buildingId: inst.BuildingId,
                unitId: inst.UnitId,
                cashBankId: inst.CashBankId,
            });
            totalDebit += +inst.DebitAmount;
            totalCredit += +inst.CreditAmount;
        }

        if (totalDebit !== totalCredit) {
            this.voucherForm.setErrors({ notbalanced: true });
            return;
        }
        this.voucher.voucherDetails = voucherDetails;
        if (this.voucher.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }
    Add() {
        this.voucher.id = uuidv4();
        this.voucherService.CreateVoucher(this.voucher).subscribe(
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
            .UpdateVoucher(this.voucher.id, this.voucher)
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

    addNewLine(
        accountId: any,
        debitAmount: any,
        creditAmount: any,
        renterId: any,
        ownerId: any,
        contributerId: any,
        buildingId: any,
        unitId: any,
        cashBankId: any
    ) {
        this.VoucherLines.push(
            this.fb.group({
                AccountId: [accountId, Validators.required],
                DebitAmount: [
                    debitAmount,
                    this.invalidValidation("CreditAmount"),
                ],
                CreditAmount: [
                    creditAmount,
                    this.invalidValidation("DebitAmount"),
                ],
                RenterId: [renterId],
                OwnerId: [ownerId],
                ContributerId: [contributerId],
                BuildingId: [buildingId],
                UnitId: [unitId],
                CashBankId: [cashBankId],
            })
        );
    }
    deleteRecord(index: number) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.VoucherLines.removeAt(index);
            }
        });
    }
    PostVoucher(id: any) {
        const Msg = this.translateService.Translate("PostMessage");
        this.dialogService.confirm(Msg).subscribe((res) => {
            if (res) {
                this.voucherService.PostVoucher(id).subscribe(
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

    invalidValidation(otherControlName: string) {
        return (
            control: AbstractControl
        ): { [key: string]: boolean } | null => {
            if (!control.parent) {
                return null;
            }
            const otherControl = control?.parent?.get(otherControlName);
            const otherValue = control?.parent?.get(otherControlName)?.value;
            const thisValue = control.value;
            const thisNotValueHasValue =
                thisValue == undefined ||
                thisValue == null ||
                thisValue == 0 ||
                thisValue == "" ||
                thisValue == "0";
            const otherNotValueHasValue =
                otherValue == undefined ||
                otherValue == null ||
                otherValue == 0 ||
                otherValue == "" ||
                otherValue == "0";
            if (thisNotValueHasValue && otherNotValueHasValue) {
                const error = {
                    required: true,
                };

                otherControl?.setErrors(error);
                return error;
            }
            if (
                thisNotValueHasValue == false &&
                otherNotValueHasValue == false
            ) {
                const error = {
                    invalid: true,
                };
                otherControl?.setErrors(error);
                return error;
            }
            otherControl?.setErrors(null);
            return null;
        };
    }

    onUnitChange(unitCTR: NgSelectComponent, buildingCTR: NgSelectComponent) {
        if (unitCTR.hasValue) {
            const unitId = unitCTR.selectedValues[0];
            const buildingId = this.units.find(
                (x) => x.id == unitId
            )?.buildingId;
            buildingCTR.writeValue(buildingId);
        } else {
            buildingCTR.writeValue(null);
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

    getControlByName(fg: FormGroup, name: string) {
        return fg.controls[name] as FormControl;
    }
}
