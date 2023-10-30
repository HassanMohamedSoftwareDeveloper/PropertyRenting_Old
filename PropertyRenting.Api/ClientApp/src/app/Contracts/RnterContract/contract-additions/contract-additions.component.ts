import {
    Component,
    OnInit,
    OnChanges,
    Output,
    Input,
    EventEmitter,
} from "@angular/core";
import { v4 as uuidv4 } from "uuid";
import {
    AbstractControl,
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { ContractFinancialTransaction } from "../../../Models/contract-financial-transaction";
import { ContractAddionsService } from "../../../Services/contract-addions.service";
import { Lookup } from "../../../Models/lookup";

@Component({
    selector: "app-contract-additions",
    templateUrl: "./contract-additions.component.html",
    styleUrls: ["./contract-additions.component.css"],
})
export class ContractAdditionsComponent implements OnInit, OnChanges {
    mandatoryId = "6d26195c-46e2-4ef2-ba84-f50174c042ea";
    contractAdditionForm!: FormGroup;
    @Input() contractAddition: ContractFinancialTransaction = { id: null };
    @Output() hideModalEvent = new EventEmitter<void>();
    @Output() addedContractAdditionEvent =
        new EventEmitter<ContractFinancialTransaction>();
    submitted = false;

    contractAdditions: Lookup[] = [];
    constructor(
        private fb: FormBuilder,
        private contractAdditionService: ContractAddionsService
    ) {}
    ngOnInit(): void {
        this.CreateFrom();
        this.loadAdditions();
    }
    ngOnChanges(): void {
        if (
            this.contractAddition.id === null &&
            this.contractAddition.contractAdditionId === null
        ) {
            this.resetFrom();
        }
    }

    loadAdditions() {
        this.contractAdditionService.GetLookup().subscribe({
            next: (res) =>
                (this.contractAdditions = res.filter(
                    (x) => x.id != this.mandatoryId
                )),
            error: (error) => console.log(error),
        });
    }

    CreateFrom() {
        this.contractAdditionForm = this.fb.group({
            ContractAdditionId: [
                null,
                [Validators.required, this.validateDropdown],
            ],
            Amount: [null, [Validators.required]],
            DueDate: [null, [Validators.required]],
        });
    }

    //#region Form Controllers
    get ContractAdditionId() {
        return this.contractAdditionForm.controls[
            "ContractAdditionId"
        ] as FormControl;
    }
    get Amount() {
        return this.contractAdditionForm.controls["Amount"] as FormControl;
    }
    get DueDate() {
        return this.contractAdditionForm.controls["DueDate"] as FormControl;
    }
    //#endregion

    onSubmit() {
        this.submitted = true;
        if (this.contractAdditionForm.invalid) {
            return;
        }
        const exp = this.contractAdditions.find(
            (x) => x.id === this.contractAddition.contractAdditionId
        );
        this.contractAddition.contractAddition = exp?.description;

        if (
            this.contractAddition.id === null &&
            this.contractAddition.tempId === null
        ) {
            this.contractAddition.tempId = uuidv4();
        }
        this.addedContractAdditionEvent.emit(this.contractAddition);
    }

    resetFrom() {
        this.contractAdditionForm?.reset();
        this.submitted = false;
    }

    cancel() {
        this.resetFrom();
        this.hideModalEvent.emit();
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
    validateDropdown(control: AbstractControl) {
        const thisValue = control.value;
        if (
            thisValue === undefined ||
            thisValue === null ||
            thisValue === "" ||
            thisValue === "null"
        ) {
            return { required: true };
        }
        return null;
    }
}
