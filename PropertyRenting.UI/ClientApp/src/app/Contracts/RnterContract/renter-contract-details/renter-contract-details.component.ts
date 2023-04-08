import { Component, OnInit, ViewChild } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { v4 as uuidv4 } from "uuid";
import { ActivatedRoute, Router } from "@angular/router";
import { Enum } from "../../../Models/enum";
import { Renter } from "../../../Models/renter";
import { RenterContract } from "../../../Models/renter-contract";
import { Unit } from "../../../Models/unit";
import { ContractService } from "../../../Services/contract.service";
import { RenterService } from "../../../Services/renter.service";
import { UnitService } from "../../../Services/unit.service";
import { RenterContractService } from "../../../Services/renter-contract.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { TranslationService } from "../../../Services/translation.service";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { ContractFinancialTransaction } from "../../../Models/contract-financial-transaction";
import { ModalComponent } from "../../../CustomTemplates/modal/modal.component";
import { BuildingService } from "../../../Services/building.service";
import { Building } from "../../../Models/building";
import { DialogService } from "../../../Services/dialog.service";
import { forkJoin } from "rxjs";

@Component({
    selector: "app-renter-contract-details",
    templateUrl: "./renter-contract-details.component.html",
    styleUrls: ["./renter-contract-details.component.css"],
})
export class RenterContractDetailsComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    contractForm!: FormGroup;
    units: Unit[] = [];
    buildings: Building[] = [];
    renters: Renter[] = [];
    submitted = false;
    increasing = "0";
    contract: RenterContract = { id: null, unitId: null, renterId: null };
    paymentMethods: Enum[] = [];

    contractAddition: ContractFinancialTransaction = {};
    @ViewChild("modal", { static: false }) modal?: ModalComponent;
    showForm = false;
    showActivateButton = true;

    constructor(
        private fb: FormBuilder,
        private renterService: RenterService,
        private unitService: UnitService,
        private router: Router,
        private route: ActivatedRoute,
        private contractService: ContractService,
        private renterContractService: RenterContractService,
        private translateService: TranslationService,
        private alertify: AlertifyService,
        private breadcrumbService: BreadcrumbService,
        private buildingservice: BuildingService,
        private dialogService: DialogService
    ) {
        this.paymentMethods = contractService.PaymentMethods;
    }

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.RenterContractDetailsItems;
        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.loadDataWithContractId(id);
        } else {
            this.loadData();
        }
    }
    loadDataWithContractId(contractId: any) {
        forkJoin(
            this.buildingservice.GetAllBuildings(),
            this.renterService.GetAll(),
            this.renterContractService.GetById(contractId)
        ).subscribe(
            ([buildingRes, renterRes, contractRes]) => {
                this.buildings = buildingRes;
                this.renters = renterRes;
                this.contract = contractRes;

                if (
                    this.contract.contractState == 2 ||
                    this.contract.contractState == 3
                ) {
                    this.router.navigate([
                        "/contracts/rentercontracts/details/" +
                            this.contract.id,
                    ]);
                } else {
                    this.increasing =
                        this.contract.increasing == true ? "1" : "0";
                    this.unitService
                        .GetAvailable(this.contract.buildingId)
                        .subscribe(
                            (res) => {
                                this.units = res;
                                this.createForm();
                            },
                            (error) => {
                                const msg =
                                    this.translateService.Translate(
                                        "ErrorOccurred"
                                    );
                                this.alertify.error(msg);
                                console.log(error);
                            }
                        );
                }
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
            this.buildingservice.GetAllBuildings(),
            this.renterService.GetAll()
        ).subscribe(
            ([buildingRes, renterRes]) => {
                this.buildings = buildingRes;
                this.renters = renterRes;
                this.createForm();
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }

    createForm() {
        this.contractForm = this.fb.group({
            AutoNumber: [null],
            ContractNumber: [null, Validators.required],
            BuildingId: [null, [Validators.required, this.validateDropdown]],
            UnitId: [null, [Validators.required, this.validateDropdown]],
            RenterId: [null, [Validators.required, this.validateDropdown]],
            Description: [null, Validators.required],
            ContractDate: [null, Validators.required],
            ContractStartDate: [null, Validators.required],
            ContractEndDate: [null, Validators.required],
            ContractAmount: [null, Validators.required],
            PaymentMethod: [null, Validators.required],
            Increasing: ["0", Validators.required],
            IncreasingValue: [
                null,
                this.requiredFieldWhenAnotherHasValue("Increasing", "1"),
            ],
        });
        this.showForm = true;
        this.contractForm?.valueChanges?.subscribe(() => {
            if (this.contractForm?.dirty) {
                this.showActivateButton = false;
            }
        });

        this.Increasing?.valueChanges.subscribe(() => {
            this.IncreasingValue?.reset();
        });
    }

    loadUnits(buildingId: any) {
        this.unitService.GetAvailable(buildingId).subscribe(
            (res) => {
                this.units = res;
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get ContractNumber() {
        return this.contractForm.controls["ContractNumber"] as FormControl;
    }
    get BuildingId() {
        return this.contractForm.controls["BuildingId"] as FormControl;
    }
    get UnitId() {
        return this.contractForm.controls["UnitId"] as FormControl;
    }
    get RenterId() {
        return this.contractForm.controls["RenterId"] as FormControl;
    }
    get Description() {
        return this.contractForm.controls["Description"] as FormControl;
    }
    get ContractDate() {
        return this.contractForm.controls["ContractDate"] as FormControl;
    }
    get ContractStartDate() {
        return this.contractForm.controls["ContractStartDate"] as FormControl;
    }
    get ContractEndDate() {
        return this.contractForm.controls["ContractEndDate"] as FormControl;
    }
    get ContractAmount() {
        return this.contractForm.controls["ContractAmount"] as FormControl;
    }
    get PaymentMethod() {
        return this.contractForm.controls["PaymentMethod"] as FormControl;
    }
    get Increasing() {
        return this.contractForm.controls["Increasing"] as FormControl;
    }
    get IncreasingValue() {
        return this.contractForm.controls["IncreasingValue"] as FormControl;
    }

    get MinDate() {
        return new Date(Date.parse(this.ContractStartDate.value));
    }

    resetFrom() {
        this.contractForm.reset();
        this.submitted = false;
    }

    EmptyEndDate(value: Date) {
        if (this.MinDate > value) {
            this.ContractEndDate.setValue(null);
        }
    }

    backToList() {
        this.router.navigate(["/contracts/rentercontracts"]);
    }

    onSubmit() {
        this.submitted = true;
        if (this.contractForm.invalid) {
            return;
        }
        this.contract.increasing = this.increasing === "1" ? true : false;
        if (this.contract.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.contract.id = uuidv4();
        this.contract.contractState = 1;
        for (const extra of this.contract.renterFinancialTransactions || []) {
            extra.id = uuidv4();
        }
        this.renterContractService.Create(this.contract).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
                this.contract.id = null;
            }
        );
    }
    private Update() {
        for (const extra of this.contract.renterFinancialTransactions || []) {
            if (extra.id === null) {
                extra.id = uuidv4();
            }
        }

        this.renterContractService
            .Edit(this.contract.id, this.contract)
            .subscribe(
                () => {
                    const successMsg = this.translateService.Translate(
                        "UpdatedSuccessfully"
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

    GetContractState(id: number): string {
        return this.contractService.GetContractStateById(id);
    }
    ActivateContract(id: any) {
        this.renterContractService.Activate(id).subscribe(
            () => {
                const successMsg = this.translateService.Translate(
                    "ActivatedSuccessfully"
                );
                this.alertify.success(successMsg);
                this.router.navigate([
                    "/contracts/rentercontracts/details/" + this.contract.id,
                ]);
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.error(msg);
                console.log(error);
            }
        );
    }

    HideModal() {
        this.modal?.hideModal();
    }

    addContractAddition() {
        this.contractAddition = { id: null, tempId: null };
        this.modal?.showModal();
    }
    editContractAddition(contractAddition: ContractFinancialTransaction) {
        this.contractAddition = { ...contractAddition };
        this.modal?.showModal();
    }

    deleteContractAddition(contractAddition: ContractFinancialTransaction) {
        const deleteMsg = this.translateService.Translate("DeleteMessage");
        this.dialogService.confirm(deleteMsg).subscribe((res) => {
            if (res) {
                this.contract.renterFinancialTransactions =
                    this.contract?.renterFinancialTransactions?.filter(
                        (x) =>
                            (x.id != null && x.id != contractAddition.id) ||
                            (x.tempId != null &&
                                x.tempId != contractAddition.tempId)
                    );
                this.showActivateButton = false;
            }
        });
    }

    addUpdateContractAddition(data: ContractFinancialTransaction) {
        this.HideModal();
        this.contract.renterFinancialTransactions =
            this.contract.renterFinancialTransactions || [];

        if (data.id != null) {
            const index = this.contract.renterFinancialTransactions.findIndex(
                (x) => x.id == data.id
            );
            if (index > -1) {
                this.contract.renterFinancialTransactions[index] = { ...data };
            }
        } else {
            const index = this.contract.renterFinancialTransactions.findIndex(
                (x) => x.tempId == data.tempId
            );
            if (index === -1) {
                this.contract.renterFinancialTransactions.push({ ...data });
            } else {
                this.contract.renterFinancialTransactions[index] = { ...data };
            }
        }
        this.showActivateButton = false;
    }

    //#region Validations :
    // Only Integer Numbers
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
    smallerThan(otherControlName: string) {
        return (
            control: AbstractControl
        ): { [key: string]: boolean } | null => {
            if (!control.parent) {
                return null; // Control is not yet associated with a parent.
            }
            const thisValue = control.value;
            const otherValue = control?.parent?.get(otherControlName)?.value;

            if (
                thisValue == undefined ||
                thisValue == null ||
                thisValue == "" ||
                thisValue == 0
            ) {
                return null;
            }
            if (+thisValue <= +otherValue) {
                return null;
            }

            return {
                smallerthan: true,
            };
        };
    }

    greaterThan(otherControlName: string) {
        return (
            control: AbstractControl
        ): { [key: string]: boolean } | null => {
            if (!control.parent) {
                return null; // Control is not yet associated with a parent.
            }
            const thisValue = control.value;
            const otherValue = control?.parent?.get(otherControlName)?.value;

            if (
                thisValue == undefined ||
                thisValue == null ||
                thisValue == "" ||
                thisValue == 0
            ) {
                return null;
            }
            if (+thisValue >= +otherValue) {
                return null;
            }

            return {
                greaterthan: true,
            };
        };
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

    requiredFieldWhenAnotherHasValue(otherControlName: string, value: any) {
        return (
            control: AbstractControl
        ): { [key: string]: boolean } | null => {
            if (!control.parent) {
                return null; // Control is not yet associated with a parent.
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

    //#endregion
}
