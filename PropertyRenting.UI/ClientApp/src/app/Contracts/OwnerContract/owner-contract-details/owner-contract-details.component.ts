import { Component, OnInit } from "@angular/core";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { v4 as uuidv4 } from "uuid";
import { ActivatedRoute, Router } from "@angular/router";
import { Building } from "../../../Models/building";
import { Enum } from "../../../Models/enum";
import { Owner } from "../../../Models/owner";
import { OwnerContract } from "../../../Models/owner-contract";
import { BuildingService } from "../../../Services/building.service";
import { ContractService } from "../../../Services/contract.service";
import { OwnerContractService } from "../../../Services/owner-contract.service";
import { OwnerService } from "../../../Services/owner.service";
import { TranslationService } from "../../../Services/translation.service";
import { AlertifyService } from "../../../Services/alertify.service";
import { Breadcrumb } from "../../../Models/breadcrumb";
import { BreadcrumbService } from "../../../Services/breadcrumb.service";
import { forkJoin } from "rxjs";

@Component({
    selector: "app-owner-contract-details",
    templateUrl: "./owner-contract-details.component.html",
    styleUrls: ["./owner-contract-details.component.css"],
})
export class OwnerContractDetailsComponent implements OnInit {
    breadcrumbItems: Breadcrumb[] = [];
    contractForm!: FormGroup;
    buildings: Building[] = [];
    owners: Owner[] = [];
    submitted = false;
    contract: OwnerContract = { id: null, buildingId: null, ownerId: null };
    paymentMethods: Enum[] = [];
    showForm = false;
    showActivateButton = true;
    constructor(
        private fb: FormBuilder,
        private ownerService: OwnerService,
        private buildingService: BuildingService,
        private router: Router,
        private route: ActivatedRoute,
        private contractService: ContractService,
        private ownerContractService: OwnerContractService,
        private translateService: TranslationService,
        private alertify: AlertifyService,
        private breadcrumbService: BreadcrumbService
    ) {
        this.paymentMethods = contractService.PaymentMethods;
    }

    ngOnInit(): void {
        this.breadcrumbItems = this.breadcrumbService.OwnerContractDetailsItems;

        const id = this.route.snapshot.paramMap.get("id");
        if (id) {
            this.loadDataWithContractId(id);
        } else {
            this.loadData();
        }
    }

    loadDataWithContractId(contractId: any) {
        forkJoin(
            this.buildingService.GetAllBuildings(),
            this.ownerService.GetAllOwners(),
            this.ownerContractService.GetById(contractId)
        ).subscribe(
            ([buildingRes, ownerRes, contractRes]) => {
                this.buildings = buildingRes;
                this.owners = ownerRes;
                this.contract = contractRes;

                if (
                    this.contract.contractState == 2 ||
                    this.contract.contractState == 3
                ) {
                    this.router.navigate([
                        "/contracts/ownercontracts/details/" + this.contract.id,
                    ]);
                }
                this.createForm();
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
            this.buildingService.GetAllBuildings(),
            this.ownerService.GetAllOwners()
        ).subscribe(
            ([buildingRes, ownerRes]) => {
                this.buildings = buildingRes;
                this.owners = ownerRes;
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
            OwnerId: [null, [Validators.required, this.validateDropdown]],
            Description: [null, Validators.required],
            ContractDate: [null, Validators.required],
            ContractStartDate: [null, Validators.required],
            ContractEndDate: [null, Validators.required],
            ContractAmount: [null, Validators.required],
            PaymentMethod: [null, Validators.required],
        });
        this.showForm = true;
        this.contractForm?.valueChanges?.subscribe(() => {
            if (this.contractForm?.dirty) {
                this.showActivateButton = false;
            }
        });
    }

    get ContractNumber() {
        return this.contractForm.controls["ContractNumber"] as FormControl;
    }
    get BuildingId() {
        return this.contractForm.controls["BuildingId"] as FormControl;
    }
    get OwnerId() {
        return this.contractForm.controls["OwnerId"] as FormControl;
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
        this.router.navigate(["/contracts/ownercontracts"]);
    }

    onSubmit() {
        this.submitted = true;
        if (this.contractForm.invalid) {
            return;
        }

        if (this.contract.id == null) {
            this.Add();
        } else {
            this.Update();
        }
    }

    private Add() {
        this.contract.id = uuidv4();
        this.contract.contractState = 1;
        this.ownerContractService.Create(this.contract).subscribe(
            () => {
                const successMsg =
                    this.translateService.Translate("AddedSuccessfully");
                this.alertify.success(successMsg);
                this.backToList();
            },
            (error) => {
                console.log(error);
                this.contract.id = null;
            }
        );
    }
    private Update() {
        this.ownerContractService
            .Edit(this.contract.id, this.contract)
            .subscribe(
                () => {
                    const successMsg = this.translateService.Translate(
                        "UpdatedSuccessfully"
                    );
                    this.alertify.success(successMsg);
                    this.backToList();
                },
                (error) => console.log(error)
            );
    }

    GetContractState(id: number): string {
        return this.contractService.GetContractStateById(id);
    }
    ActivateContract(id: any) {
        this.ownerContractService.Activate(id).subscribe(
            () => {
                const successMsg = this.translateService.Translate(
                    "ActivatedSuccessfully"
                );
                this.alertify.success(successMsg);
                this.router.navigate([
                    "/contracts/ownercontracts/details/" + this.contract.id,
                ]);
            },
            (error) => {
                const msg = this.translateService.Translate("ErrorOccurred");
                this.alertify.success(msg);
                console.log(error);
            }
        );
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
    //#endregion
}
