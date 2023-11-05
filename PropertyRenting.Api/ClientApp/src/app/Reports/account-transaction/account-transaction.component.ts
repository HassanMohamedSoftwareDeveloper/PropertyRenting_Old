import { DatePipe } from "@angular/common";
import { Component, ElementRef, OnInit, ViewChild } from "@angular/core";
import {
    FormBuilder,
    FormControl,
    FormGroup,
    Validators,
} from "@angular/forms";
import { Breadcrumb } from "../../Models/breadcrumb";
import { AccountTransaction } from "../../Models/Reports/account-transaction";
import { AccountService } from "../../Services/account.service";
import { AlertifyService } from "../../Services/alertify.service";
import { BreadcrumbService } from "../../Services/breadcrumb.service";
import { ReportService } from "../../Services/report.service";
import { TranslationService } from "../../Services/translation.service";
import { AccountLookup } from "../../Models/account-lookup";
import jsPDF from "jspdf";
import html2canvas from "html2canvas";
//import * as pdfMake from "pdfmake/build/pdfmake";
//import * as pdfFonts from "pdfmake/build/vfs_fonts";
//import { htmlToPdfmake } from "html-to-pdfmake";
//(<any>pdfMake).addVirtualFileSystem(pdfFonts);

@Component({
    selector: "app-account-transaction",
    templateUrl: "./account-transaction.component.html",
    styleUrls: ["./account-transaction.component.css"],
})
export class AccountTransactionComponent implements OnInit {
    data: AccountTransaction[] = [];
    accounts: AccountLookup[] = [];
    breadcrumbItems: Breadcrumb[] = [];
    filterForm!: FormGroup;
    showReport = false;
    @ViewChild("testId", { static: false }) el!: ElementRef;
    constructor(
        private reportService: ReportService,
        private alertify: AlertifyService,
        private translateService: TranslationService,
        private breadcrumbService: BreadcrumbService,
        private fb: FormBuilder,
        private accountService: AccountService
    ) {}

    ngOnInit(): void {
        this.breadcrumbItems =
            this.breadcrumbService.AccountTransactionReportItems;

        this.createForm();
        this.loadAccounts();
    }
    createForm() {
        this.filterForm = this.fb.group({
            AccountId: [null, Validators.required],
            FromDate: [null],
            ToDate: [null],
        });
    }

    loadAccounts() {
        this.accountService.GetLookup().subscribe(
            (res) => {
                this.accounts = res.filter((x) => x.accountTypeId != 5);
            },
            (error) => {
                console.log(error);
            }
        );
    }

    get AccountId() {
        return this.filterForm.controls["AccountId"] as FormControl;
    }

    get FromDate() {
        return this.filterForm.controls["FromDate"] as FormControl;
    }
    get ToDate() {
        return this.filterForm.controls["ToDate"] as FormControl;
    }

    get MinDate() {
        return new Date(Date.parse(this.FromDate.value));
    }
    EmptyToDate(value: Date) {
        if (this.MinDate > value) {
            this.ToDate.setValue(null);
        }
    }

    loadReport() {
        this.showReport = false;
        this.reportService
            .GetAccountTransactions(
                this.AccountId.value,
                this.FromDate.value,
                this.ToDate.value
            )
            .subscribe(
                (res) => {
                    this.data = res;
                    this.showReport = true;
                },
                (error) => {
                    const msg =
                        this.translateService.Translate("ErrorOccurred");
                    this.alertify.error(msg);
                    console.log(error);
                }
            );
    }
    onSubmit() {
        this.loadReport();
    }

    exportPDF() {
        const pdfMake = require("pdfmake/build/pdfmake");
        const pdfFonts = require("pdfmake/build/vfs_fonts");
        pdfMake.vfs = pdfFonts.pdfMake.vfs;
        const htmlToPdfmake = require("html-to-pdfmake");
        const pdfTable = this.el.nativeElement;

        const html = htmlToPdfmake(pdfTable.innerHTML);

        const documentDefinition = { content: html };
        pdfMake.createPdf(documentDefinition).open();
        //html2canvas(this.el.nativeElement, { scale: 3 }).then((canvas) => {
        //    const imageGeneratedFromTemplate = canvas.toDataURL("image/png");
        //    const fileWidth = 200;
        //    const generatedImageHeight =
        //        (canvas.height * fileWidth) / canvas.width;
        //    const PDF = new jsPDF("p", "mm", "a4");

        //    PDF.addImage(
        //        imageGeneratedFromTemplate,
        //        "PNG",
        //        0,
        //        5,
        //        fileWidth,
        //        generatedImageHeight
        //    );
        //    PDF.html(this.el.nativeElement.innerHTML);
        //    PDF.html(this.el.nativeElement.innerHTML, {
        //        callback: (pdf) => {
        //            pdf.save("angular-invoice-pdf-demo.pdf");
        //            pdf.output("dataurl", { filename: "test.pdf" });
        //        },
        //    });
        //    PDF.save("angular-invoice-pdf-demo.pdf");
        //    PDF.output("dataurl", { filename: "test.pdf" });
        //});
        //const accountName =
        //    this.accounts.find((x) => x.id == this.AccountId.value)
        //        ?.description || "";
        //this.reportService
        //    .ExportAccountTransactions(
        //        "PDF",
        //        this.AccountId.value,
        //        accountName,
        //        this.FromDate.value,
        //        this.ToDate.value
        //    )
        //    .subscribe(
        //        (blob) => {
        //            const today = new Date();
        //            const pipe = new DatePipe("en-US");
        //            const ChangedFormat = pipe.transform(
        //                today,
        //                "dd-MM-YYYY_HH-mm-ss"
        //            );
        //            const a = document.createElement("a");
        //            const objectUrl = URL.createObjectURL(blob);
        //            a.href = objectUrl;
        //            a.download = "AccountTransaction_" + ChangedFormat + ".pdf";
        //            a.target = "_blank";
        //            a.click();
        //            URL.revokeObjectURL(objectUrl);
        //        },
        //        (error) => {
        //            console.log(error);
        //        }
        //    );
    }
    exportExcel() {
        const accountName =
            this.accounts.find((x) => x.id == this.AccountId.value)
                ?.description || "";
        this.reportService
            .ExportAccountTransactions(
                "EXCEL",
                this.AccountId.value,
                accountName,
                this.FromDate.value,
                this.ToDate.value
            )
            .subscribe(
                (blob) => {
                    const today = new Date();
                    const pipe = new DatePipe("en-US");
                    const ChangedFormat = pipe.transform(
                        today,
                        "dd-MM-YYYY_HH-mm-ss"
                    );
                    const a = document.createElement("a");
                    const objectUrl = URL.createObjectURL(blob);
                    a.href = objectUrl;
                    a.download = "AccountTransaction_" + ChangedFormat + ".xls";
                    a.target = "_blank";
                    a.click();
                    URL.revokeObjectURL(objectUrl);
                },
                (error) => {
                    console.log(error);
                }
            );
    }
}
