import { Component, Output, EventEmitter } from "@angular/core";
import { AppInfoDialogComponent } from "./app-info-dialog/app-info-dialog.component";
import { BehaviorSubject } from "rxjs";
import { MatDialog } from "@angular/material/dialog";
import { MatSelectChange } from "@angular/material/select";

// TODO: improve buttons
// TODO: fix console warnings

@Component({
  selector: "eg-qr-reader",
  templateUrl: "./qr-reader.component.html",
  styleUrls: ["./qr-reader.component.scss"],
})
export class QrReaderComponent {
  @Output() codeResult: EventEmitter<string> = new EventEmitter();

  done = false;
  scannerEnabled: boolean;

  availableDevices: MediaDeviceInfo[];
  currentDevice: MediaDeviceInfo = null;
  currentDeviceId: string;

  hasDevices: boolean;
  hasPermission: boolean;

  torchEnabled = false;
  torchAvailable$ = new BehaviorSubject<boolean>(false);

  constructor(private readonly _dialog: MatDialog) {}

  onScanSuccess(result: string) {
    this.done = true;
    this.scannerEnabled = false;
    this.codeResult.emit(result);
  }

  onPlayStopClicked() {
    this.scannerEnabled = !this.scannerEnabled;
    this.done = false;
  }

  onCamerasFound(devices: MediaDeviceInfo[]): void {
    this.availableDevices = devices;
    this.hasDevices = Boolean(devices && devices.length);

    this.currentDevice = devices[0];
    this.currentDeviceId = this.currentDevice?.deviceId;
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
