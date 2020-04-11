import { Component, Output, EventEmitter } from "@angular/core";
import { AppInfoDialogComponent } from "./app-info-dialog/app-info-dialog.component";
import { BehaviorSubject } from "rxjs";
import { MatDialog } from "@angular/material/dialog";
import { ResultAndError } from "@zxing/ngx-scanner/lib/ResultAndError";
import { MatSelectChange } from "@angular/material/select";

// TODO: improve buttons
// TODO: fix console warnings

@Component({
  selector: "eg-qr-reader",
  templateUrl: "./qr-reader.component.html",
  styleUrls: ["./qr-reader.component.scss"],
})
export class QrReaderComponent {
  @Output() qrResult: EventEmitter<string>;

  scannerEnabled: boolean;

  availableDevices: MediaDeviceInfo[];
  currentDevice: MediaDeviceInfo = null;
  currentDeviceId: string;

  hasDevices: boolean;
  hasPermission: boolean;

  torchEnabled = false;
  torchAvailable$ = new BehaviorSubject<boolean>(false);

  constructor(private readonly _dialog: MatDialog) {}

  onCodeResult(result: string) {
    this.scannerEnabled = false;
    this.qrResult.emit(result);
  }

  onPlayStopClicked() {
    this.scannerEnabled = !this.scannerEnabled;
  }

  onCamerasFound(devices: MediaDeviceInfo[]): void {
    this.availableDevices = devices;
    // this.currentDeviceId = this.currentDevice?.deviceId;
    this.hasDevices = Boolean(devices && devices.length);
  }

  onDeviceSelectChange(selected: MatSelectChange) {
    const device = this.availableDevices.find(
      (x) => x.deviceId === selected.value
    );
    this.currentDevice = device || null;
    this.currentDeviceId = this.currentDevice?.deviceId;
  }

  onHasPermission(has: boolean) {
    this.hasPermission = has;
  }

  // Info Dialog

  openInfoDialog() {
    const data = {
      hasDevices: this.hasDevices,
      hasPermission: this.hasPermission,
    };

    this._dialog.open(AppInfoDialogComponent, { data });
  }

  // Turn on/off the device flashlight.

  onTorchCompatible(isCompatible: boolean) {
    this.torchAvailable$.next(isCompatible || false);
  }

  toggleTorch() {
    this.torchEnabled = !this.torchEnabled;
  }
}
