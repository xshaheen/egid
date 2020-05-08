/* tslint:disable */

import { mergeMap as _observableMergeMap, catchError as _observableCatch } from 'rxjs/operators';
import { Observable, throwError as _observableThrow, of as _observableOf } from 'rxjs';
import { Injectable, Inject, Optional, InjectionToken } from '@angular/core';
import { HttpClient, HttpHeaders, HttpResponse, HttpResponseBase } from '@angular/common/http';

export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

@Injectable({
    providedIn: 'root'
})
export class AuthClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    signIn(command: LoginCommand): Observable<string> {
        let url_ = this.baseUrl + "/api/auth";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processSignIn(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processSignIn(<any>response_);
                } catch (e) {
                    return <Observable<string>><any>_observableThrow(e);
                }
            } else
                return <Observable<string>><any>_observableThrow(response_);
        }));
    }

    protected processSignIn(response: HttpResponseBase): Observable<string> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }
}

@Injectable({
    providedIn: 'root'
})
export class CardClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    post(command: CreateCardCommand): Observable<string> {
        let url_ = this.baseUrl + "/api/card";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPost(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPost(<any>response_);
                } catch (e) {
                    return <Observable<string>><any>_observableThrow(e);
                }
            } else
                return <Observable<string>><any>_observableThrow(response_);
        }));
    }

    protected processPost(response: HttpResponseBase): Observable<string> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }

    puk(command: ChangePukCommand): Observable<void> {
        let url_ = this.baseUrl + "/api/card/puk";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPuk(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPuk(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processPuk(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }

    pin1(command: ChangePin1Command): Observable<void> {
        let url_ = this.baseUrl + "/api/card/pin1";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPin1(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPin1(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processPin1(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }

    pin2(command: ChangePin2Command): Observable<void> {
        let url_ = this.baseUrl + "/api/card/pin2";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPin2(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPin2(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processPin2(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }
}

@Injectable({
    providedIn: 'root'
})
export class CitizensClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    getAll(): Observable<CitizenDetailsVm[]> {
        let url_ = this.baseUrl + "/api/citizens";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAll(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAll(<any>response_);
                } catch (e) {
                    return <Observable<CitizenDetailsVm[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<CitizenDetailsVm[]>><any>_observableThrow(response_);
        }));
    }

    protected processGetAll(response: HttpResponseBase): Observable<CitizenDetailsVm[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(CitizenDetailsVm.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<CitizenDetailsVm[]>(<any>null);
    }

    post(command: CreateCitizenCommand): Observable<string> {
        let url_ = this.baseUrl + "/api/citizens";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPost(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPost(<any>response_);
                } catch (e) {
                    return <Observable<string>><any>_observableThrow(e);
                }
            } else
                return <Observable<string>><any>_observableThrow(response_);
        }));
    }

    protected processPost(response: HttpResponseBase): Observable<string> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }

    getOne(id: string | null, query: GetCitizenDetailsQuery): Observable<void> {
        let url_ = this.baseUrl + "/api/citizens/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(query);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetOne(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetOne(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processGetOne(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }

    update(id: string | null, command: UpdateCitizenCommand): Observable<void> {
        let url_ = this.baseUrl + "/api/citizens/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processUpdate(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processUpdate(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processUpdate(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }

    delete(id: string | null): Observable<void> {
        let url_ = this.baseUrl + "/api/citizens/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDelete(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDelete(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processDelete(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<void>(<any>null);
    }
}

@Injectable({
    providedIn: 'root'
})
export class EmployeesClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    getAll(): Observable<EmployeesVm[]> {
        let url_ = this.baseUrl + "/api/employees";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetAll(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetAll(<any>response_);
                } catch (e) {
                    return <Observable<EmployeesVm[]>><any>_observableThrow(e);
                }
            } else
                return <Observable<EmployeesVm[]>><any>_observableThrow(response_);
        }));
    }

    protected processGetAll(response: HttpResponseBase): Observable<EmployeesVm[]> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            if (Array.isArray(resultData200)) {
                result200 = [] as any;
                for (let item of resultData200)
                    result200!.push(EmployeesVm.fromJS(item));
            }
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<EmployeesVm[]>(<any>null);
    }

    post(command: AddEmployeeCommand): Observable<void> {
        let url_ = this.baseUrl + "/api/employees";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPost(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPost(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processPost(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }

    delete(id: string | null): Observable<void> {
        let url_ = this.baseUrl + "/api/employees/{id}";
        if (id === undefined || id === null)
            throw new Error("The parameter 'id' must be defined.");
        url_ = url_.replace("{id}", encodeURIComponent("" + id));
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
            })
        };

        return this.http.request("delete", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processDelete(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processDelete(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processDelete(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }
}

@Injectable({
    providedIn: 'root'
})
export class HealthInfoClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    getOne(citizenId: string | null | undefined): Observable<HealthInfoVm> {
        let url_ = this.baseUrl + "/api/healthinfo?";
        if (citizenId !== undefined)
            url_ += "citizenId=" + encodeURIComponent("" + citizenId) + "&";
        url_ = url_.replace(/[?&]$/, "");

        let options_ : any = {
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Accept": "application/json"
            })
        };

        return this.http.request("get", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processGetOne(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processGetOne(<any>response_);
                } catch (e) {
                    return <Observable<HealthInfoVm>><any>_observableThrow(e);
                }
            } else
                return <Observable<HealthInfoVm>><any>_observableThrow(response_);
        }));
    }

    protected processGetOne(response: HttpResponseBase): Observable<HealthInfoVm> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = HealthInfoVm.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<HealthInfoVm>(<any>null);
    }

    post(command: AddHealthRecordCommand): Observable<void> {
        let url_ = this.baseUrl + "/api/healthinfo";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processPost(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processPost(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processPost(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }

    emergencyPhones(command: UpdateEmergencyPhonesCommand): Observable<void> {
        let url_ = this.baseUrl + "/api/healthinfo/emergencyphones";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processEmergencyPhones(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processEmergencyPhones(<any>response_);
                } catch (e) {
                    return <Observable<void>><any>_observableThrow(e);
                }
            } else
                return <Observable<void>><any>_observableThrow(response_);
        }));
    }

    protected processEmergencyPhones(response: HttpResponseBase): Observable<void> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return _observableOf<void>(<any>null);
            }));
        } else if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let resultdefault: any = null;
            let resultDatadefault = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            resultdefault = ProblemDetails.fromJS(resultDatadefault);
            return throwException("A server side error occurred.", status, _responseText, _headers, resultdefault);
            }));
        }
    }
}

@Injectable({
    providedIn: 'root'
})
export class SignatureClient {
    private http: HttpClient;
    private baseUrl: string;
    protected jsonParseReviver: ((key: string, value: any) => any) | undefined = undefined;

    constructor(@Inject(HttpClient) http: HttpClient, @Optional() @Inject(API_BASE_URL) baseUrl?: string) {
        this.http = http;
        this.baseUrl = baseUrl ? baseUrl : "";
    }

    sign(command: SignHashCommand): Observable<string> {
        let url_ = this.baseUrl + "/api/signature/sign";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processSign(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processSign(<any>response_);
                } catch (e) {
                    return <Observable<string>><any>_observableThrow(e);
                }
            } else
                return <Observable<string>><any>_observableThrow(response_);
        }));
    }

    protected processSign(response: HttpResponseBase): Observable<string> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = resultData200 !== undefined ? resultData200 : <any>null;
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<string>(<any>null);
    }

    verify(command: VerifySignatureCommand): Observable<VerifySignatureResult> {
        let url_ = this.baseUrl + "/api/signature/verify";
        url_ = url_.replace(/[?&]$/, "");

        const content_ = JSON.stringify(command);

        let options_ : any = {
            body: content_,
            observe: "response",
            responseType: "blob",
            headers: new HttpHeaders({
                "Content-Type": "application/json",
                "Accept": "application/json"
            })
        };

        return this.http.request("post", url_, options_).pipe(_observableMergeMap((response_ : any) => {
            return this.processVerify(response_);
        })).pipe(_observableCatch((response_: any) => {
            if (response_ instanceof HttpResponseBase) {
                try {
                    return this.processVerify(<any>response_);
                } catch (e) {
                    return <Observable<VerifySignatureResult>><any>_observableThrow(e);
                }
            } else
                return <Observable<VerifySignatureResult>><any>_observableThrow(response_);
        }));
    }

    protected processVerify(response: HttpResponseBase): Observable<VerifySignatureResult> {
        const status = response.status;
        const responseBlob =
            response instanceof HttpResponse ? response.body :
            (<any>response).error instanceof Blob ? (<any>response).error : undefined;

        let _headers: any = {}; if (response.headers) { for (let key of response.headers.keys()) { _headers[key] = response.headers.get(key); }}
        if (status === 404) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result404: any = null;
            let resultData404 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result404 = ProblemDetails.fromJS(resultData404);
            return throwException("A server side error occurred.", status, _responseText, _headers, result404);
            }));
        } else if (status === 200) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            let result200: any = null;
            let resultData200 = _responseText === "" ? null : JSON.parse(_responseText, this.jsonParseReviver);
            result200 = VerifySignatureResult.fromJS(resultData200);
            return _observableOf(result200);
            }));
        } else if (status !== 200 && status !== 204) {
            return blobToText(responseBlob).pipe(_observableMergeMap(_responseText => {
            return throwException("An unexpected server error occurred.", status, _responseText, _headers);
            }));
        }
        return _observableOf<VerifySignatureResult>(<any>null);
    }
}

export class ProblemDetails implements IProblemDetails {
    type?: string | null;
    title?: string | null;
    status?: number | null;
    detail?: string | null;
    instance?: string | null;
    extensions?: { [key: string]: any; } | null;

    constructor(data?: IProblemDetails) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.type = _data["type"] !== undefined ? _data["type"] : <any>null;
            this.title = _data["title"] !== undefined ? _data["title"] : <any>null;
            this.status = _data["status"] !== undefined ? _data["status"] : <any>null;
            this.detail = _data["detail"] !== undefined ? _data["detail"] : <any>null;
            this.instance = _data["instance"] !== undefined ? _data["instance"] : <any>null;
            if (_data["extensions"]) {
                this.extensions = {} as any;
                for (let key in _data["extensions"]) {
                    if (_data["extensions"].hasOwnProperty(key))
                        this.extensions![key] = _data["extensions"][key];
                }
            }
        }
    }

    static fromJS(data: any): ProblemDetails {
        data = typeof data === 'object' ? data : {};
        let result = new ProblemDetails();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["type"] = this.type !== undefined ? this.type : <any>null;
        data["title"] = this.title !== undefined ? this.title : <any>null;
        data["status"] = this.status !== undefined ? this.status : <any>null;
        data["detail"] = this.detail !== undefined ? this.detail : <any>null;
        data["instance"] = this.instance !== undefined ? this.instance : <any>null;
        if (this.extensions) {
            data["extensions"] = {};
            for (let key in this.extensions) {
                if (this.extensions.hasOwnProperty(key))
                    data["extensions"][key] = this.extensions[key] !== undefined ? this.extensions[key] : <any>null;
            }
        }
        return data;
    }
}

export interface IProblemDetails {
    type?: string | null;
    title?: string | null;
    status?: number | null;
    detail?: string | null;
    instance?: string | null;
    extensions?: { [key: string]: any; } | null;
}

export class LoginCommand implements ILoginCommand {
    cardId!: string;
    pin1!: string;

    constructor(data?: ILoginCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.cardId = _data["cardId"] !== undefined ? _data["cardId"] : <any>null;
            this.pin1 = _data["pin1"] !== undefined ? _data["pin1"] : <any>null;
        }
    }

    static fromJS(data: any): LoginCommand {
        data = typeof data === 'object' ? data : {};
        let result = new LoginCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["cardId"] = this.cardId !== undefined ? this.cardId : <any>null;
        data["pin1"] = this.pin1 !== undefined ? this.pin1 : <any>null;
        return data;
    }
}

export interface ILoginCommand {
    cardId: string;
    pin1: string;
}

export class CreateCardCommand implements ICreateCardCommand {
    ownerId!: string;
    puk!: string;
    pin1!: string;
    pin2!: string;
    email?: string | null;
    phoneNumber?: string | null;

    constructor(data?: ICreateCardCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.ownerId = _data["ownerId"] !== undefined ? _data["ownerId"] : <any>null;
            this.puk = _data["puk"] !== undefined ? _data["puk"] : <any>null;
            this.pin1 = _data["pin1"] !== undefined ? _data["pin1"] : <any>null;
            this.pin2 = _data["pin2"] !== undefined ? _data["pin2"] : <any>null;
            this.email = _data["email"] !== undefined ? _data["email"] : <any>null;
            this.phoneNumber = _data["phoneNumber"] !== undefined ? _data["phoneNumber"] : <any>null;
        }
    }

    static fromJS(data: any): CreateCardCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CreateCardCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["ownerId"] = this.ownerId !== undefined ? this.ownerId : <any>null;
        data["puk"] = this.puk !== undefined ? this.puk : <any>null;
        data["pin1"] = this.pin1 !== undefined ? this.pin1 : <any>null;
        data["pin2"] = this.pin2 !== undefined ? this.pin2 : <any>null;
        data["email"] = this.email !== undefined ? this.email : <any>null;
        data["phoneNumber"] = this.phoneNumber !== undefined ? this.phoneNumber : <any>null;
        return data;
    }
}

export interface ICreateCardCommand {
    ownerId: string;
    puk: string;
    pin1: string;
    pin2: string;
    email?: string | null;
    phoneNumber?: string | null;
}

export class ChangePukCommand implements IChangePukCommand {
    cardId!: string;
    currentPuk!: string;
    newPuk!: string;

    constructor(data?: IChangePukCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.cardId = _data["cardId"] !== undefined ? _data["cardId"] : <any>null;
            this.currentPuk = _data["currentPuk"] !== undefined ? _data["currentPuk"] : <any>null;
            this.newPuk = _data["newPuk"] !== undefined ? _data["newPuk"] : <any>null;
        }
    }

    static fromJS(data: any): ChangePukCommand {
        data = typeof data === 'object' ? data : {};
        let result = new ChangePukCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["cardId"] = this.cardId !== undefined ? this.cardId : <any>null;
        data["currentPuk"] = this.currentPuk !== undefined ? this.currentPuk : <any>null;
        data["newPuk"] = this.newPuk !== undefined ? this.newPuk : <any>null;
        return data;
    }
}

export interface IChangePukCommand {
    cardId: string;
    currentPuk: string;
    newPuk: string;
}

export class ChangePin1Command implements IChangePin1Command {
    cardId!: string;
    puk!: string;
    newPin1!: string;

    constructor(data?: IChangePin1Command) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.cardId = _data["cardId"] !== undefined ? _data["cardId"] : <any>null;
            this.puk = _data["puk"] !== undefined ? _data["puk"] : <any>null;
            this.newPin1 = _data["newPin1"] !== undefined ? _data["newPin1"] : <any>null;
        }
    }

    static fromJS(data: any): ChangePin1Command {
        data = typeof data === 'object' ? data : {};
        let result = new ChangePin1Command();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["cardId"] = this.cardId !== undefined ? this.cardId : <any>null;
        data["puk"] = this.puk !== undefined ? this.puk : <any>null;
        data["newPin1"] = this.newPin1 !== undefined ? this.newPin1 : <any>null;
        return data;
    }
}

export interface IChangePin1Command {
    cardId: string;
    puk: string;
    newPin1: string;
}

export class ChangePin2Command implements IChangePin2Command {
    cardId!: string;
    puk!: string;
    newPin2!: string;

    constructor(data?: IChangePin2Command) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.cardId = _data["cardId"] !== undefined ? _data["cardId"] : <any>null;
            this.puk = _data["puk"] !== undefined ? _data["puk"] : <any>null;
            this.newPin2 = _data["newPin2"] !== undefined ? _data["newPin2"] : <any>null;
        }
    }

    static fromJS(data: any): ChangePin2Command {
        data = typeof data === 'object' ? data : {};
        let result = new ChangePin2Command();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["cardId"] = this.cardId !== undefined ? this.cardId : <any>null;
        data["puk"] = this.puk !== undefined ? this.puk : <any>null;
        data["newPin2"] = this.newPin2 !== undefined ? this.newPin2 : <any>null;
        return data;
    }
}

export interface IChangePin2Command {
    cardId: string;
    puk: string;
    newPin2: string;
}

export class AuditableEntity implements IAuditableEntity {
    create?: Date;
    createBy?: string | null;
    lastModified?: Date | null;
    lastModifiedBy?: string | null;

    constructor(data?: IAuditableEntity) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.create = _data["create"] ? new Date(_data["create"].toString()) : <any>null;
            this.createBy = _data["createBy"] !== undefined ? _data["createBy"] : <any>null;
            this.lastModified = _data["lastModified"] ? new Date(_data["lastModified"].toString()) : <any>null;
            this.lastModifiedBy = _data["lastModifiedBy"] !== undefined ? _data["lastModifiedBy"] : <any>null;
        }
    }

    static fromJS(data: any): AuditableEntity {
        data = typeof data === 'object' ? data : {};
        let result = new AuditableEntity();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["create"] = this.create ? this.create.toISOString() : <any>null;
        data["createBy"] = this.createBy !== undefined ? this.createBy : <any>null;
        data["lastModified"] = this.lastModified ? this.lastModified.toISOString() : <any>null;
        data["lastModifiedBy"] = this.lastModifiedBy !== undefined ? this.lastModifiedBy : <any>null;
        return data;
    }
}

export interface IAuditableEntity {
    create?: Date;
    createBy?: string | null;
    lastModified?: Date | null;
    lastModifiedBy?: string | null;
}

export class CitizenDetailsVm extends AuditableEntity implements ICitizenDetailsVm {
    id?: string | null;
    fatherId?: string | null;
    motherId?: string | null;
    fullName?: FullName | null;
    address?: Address | null;
    gender?: Gender;
    religion?: Religion;
    socialStatus?: SocialStatus;
    dateOfBirth?: Date;
    photoUrl?: string | null;

    constructor(data?: ICitizenDetailsVm) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.id = _data["id"] !== undefined ? _data["id"] : <any>null;
            this.fatherId = _data["fatherId"] !== undefined ? _data["fatherId"] : <any>null;
            this.motherId = _data["motherId"] !== undefined ? _data["motherId"] : <any>null;
            this.fullName = _data["fullName"] ? FullName.fromJS(_data["fullName"]) : <any>null;
            this.address = _data["address"] ? Address.fromJS(_data["address"]) : <any>null;
            this.gender = _data["gender"] !== undefined ? _data["gender"] : <any>null;
            this.religion = _data["religion"] !== undefined ? _data["religion"] : <any>null;
            this.socialStatus = _data["socialStatus"] !== undefined ? _data["socialStatus"] : <any>null;
            this.dateOfBirth = _data["dateOfBirth"] ? new Date(_data["dateOfBirth"].toString()) : <any>null;
            this.photoUrl = _data["photoUrl"] !== undefined ? _data["photoUrl"] : <any>null;
        }
    }

    static fromJS(data: any): CitizenDetailsVm {
        data = typeof data === 'object' ? data : {};
        let result = new CitizenDetailsVm();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id !== undefined ? this.id : <any>null;
        data["fatherId"] = this.fatherId !== undefined ? this.fatherId : <any>null;
        data["motherId"] = this.motherId !== undefined ? this.motherId : <any>null;
        data["fullName"] = this.fullName ? this.fullName.toJSON() : <any>null;
        data["address"] = this.address ? this.address.toJSON() : <any>null;
        data["gender"] = this.gender !== undefined ? this.gender : <any>null;
        data["religion"] = this.religion !== undefined ? this.religion : <any>null;
        data["socialStatus"] = this.socialStatus !== undefined ? this.socialStatus : <any>null;
        data["dateOfBirth"] = this.dateOfBirth ? this.dateOfBirth.toISOString() : <any>null;
        data["photoUrl"] = this.photoUrl !== undefined ? this.photoUrl : <any>null;
        super.toJSON(data);
        return data;
    }
}

export interface ICitizenDetailsVm extends IAuditableEntity {
    id?: string | null;
    fatherId?: string | null;
    motherId?: string | null;
    fullName?: FullName | null;
    address?: Address | null;
    gender?: Gender;
    religion?: Religion;
    socialStatus?: SocialStatus;
    dateOfBirth?: Date;
    photoUrl?: string | null;
}

export abstract class ValueObject implements IValueObject {

    constructor(data?: IValueObject) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
    }

    static fromJS(data: any): ValueObject {
        data = typeof data === 'object' ? data : {};
        throw new Error("The abstract class 'ValueObject' cannot be instantiated.");
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        return data;
    }
}

export interface IValueObject {
}

export class FullName extends ValueObject implements IFullName {
    firstName?: string | null;
    secondName?: string | null;
    thirdName?: string | null;
    lastName?: string | null;

    constructor(data?: IFullName) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.firstName = _data["firstName"] !== undefined ? _data["firstName"] : <any>null;
            this.secondName = _data["secondName"] !== undefined ? _data["secondName"] : <any>null;
            this.thirdName = _data["thirdName"] !== undefined ? _data["thirdName"] : <any>null;
            this.lastName = _data["lastName"] !== undefined ? _data["lastName"] : <any>null;
        }
    }

    static fromJS(data: any): FullName {
        data = typeof data === 'object' ? data : {};
        let result = new FullName();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["firstName"] = this.firstName !== undefined ? this.firstName : <any>null;
        data["secondName"] = this.secondName !== undefined ? this.secondName : <any>null;
        data["thirdName"] = this.thirdName !== undefined ? this.thirdName : <any>null;
        data["lastName"] = this.lastName !== undefined ? this.lastName : <any>null;
        super.toJSON(data);
        return data;
    }
}

export interface IFullName extends IValueObject {
    firstName?: string | null;
    secondName?: string | null;
    thirdName?: string | null;
    lastName?: string | null;
}

export class Address extends ValueObject implements IAddress {
    street?: string | null;
    city?: string | null;
    state?: string | null;
    postalCode?: string | null;
    country?: string | null;

    constructor(data?: IAddress) {
        super(data);
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.street = _data["street"] !== undefined ? _data["street"] : <any>null;
            this.city = _data["city"] !== undefined ? _data["city"] : <any>null;
            this.state = _data["state"] !== undefined ? _data["state"] : <any>null;
            this.postalCode = _data["postalCode"] !== undefined ? _data["postalCode"] : <any>null;
            this.country = _data["country"] !== undefined ? _data["country"] : <any>null;
        }
    }

    static fromJS(data: any): Address {
        data = typeof data === 'object' ? data : {};
        let result = new Address();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["street"] = this.street !== undefined ? this.street : <any>null;
        data["city"] = this.city !== undefined ? this.city : <any>null;
        data["state"] = this.state !== undefined ? this.state : <any>null;
        data["postalCode"] = this.postalCode !== undefined ? this.postalCode : <any>null;
        data["country"] = this.country !== undefined ? this.country : <any>null;
        super.toJSON(data);
        return data;
    }
}

export interface IAddress extends IValueObject {
    street?: string | null;
    city?: string | null;
    state?: string | null;
    postalCode?: string | null;
    country?: string | null;
}

/** 0 = None 1 = Male 2 = Female */
export enum Gender {
    None = 0,
    Male = 1,
    Female = 2,
}

/** 0 = None 1 = Muslim 2 = Christian */
export enum Religion {
    None = 0,
    Muslim = 1,
    Christian = 2,
}

/** 0 = None 1 = Single 2 = Married */
export enum SocialStatus {
    None = 0,
    Single = 1,
    Married = 2,
}

export class GetCitizenDetailsQuery implements IGetCitizenDetailsQuery {
    id?: string | null;

    constructor(data?: IGetCitizenDetailsQuery) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"] !== undefined ? _data["id"] : <any>null;
        }
    }

    static fromJS(data: any): GetCitizenDetailsQuery {
        data = typeof data === 'object' ? data : {};
        let result = new GetCitizenDetailsQuery();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id !== undefined ? this.id : <any>null;
        return data;
    }
}

export interface IGetCitizenDetailsQuery {
    id?: string | null;
}

export class CreateCitizenCommand implements ICreateCitizenCommand {
    fatherId?: string | null;
    motherId?: string | null;
    fullName!: FullName;
    address!: Address;
    gender?: Gender;
    religion?: Religion;
    socialStatus?: SocialStatus;
    dateOfBirth?: Date;
    photo?: BinaryFile | null;
    bloodType?: BloodType;
    healthEmergencyPhone1?: string | null;
    healthEmergencyPhone2?: string | null;
    healthEmergencyPhone3?: string | null;

    constructor(data?: ICreateCitizenCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
        if (!data) {
            this.fullName = new FullName();
            this.address = new Address();
        }
    }

    init(_data?: any) {
        if (_data) {
            this.fatherId = _data["fatherId"] !== undefined ? _data["fatherId"] : <any>null;
            this.motherId = _data["motherId"] !== undefined ? _data["motherId"] : <any>null;
            this.fullName = _data["fullName"] ? FullName.fromJS(_data["fullName"]) : new FullName();
            this.address = _data["address"] ? Address.fromJS(_data["address"]) : new Address();
            this.gender = _data["gender"] !== undefined ? _data["gender"] : <any>null;
            this.religion = _data["religion"] !== undefined ? _data["religion"] : <any>null;
            this.socialStatus = _data["socialStatus"] !== undefined ? _data["socialStatus"] : <any>null;
            this.dateOfBirth = _data["dateOfBirth"] ? new Date(_data["dateOfBirth"].toString()) : <any>null;
            this.photo = _data["photo"] ? BinaryFile.fromJS(_data["photo"]) : <any>null;
            this.bloodType = _data["bloodType"] !== undefined ? _data["bloodType"] : <any>null;
            this.healthEmergencyPhone1 = _data["healthEmergencyPhone1"] !== undefined ? _data["healthEmergencyPhone1"] : <any>null;
            this.healthEmergencyPhone2 = _data["healthEmergencyPhone2"] !== undefined ? _data["healthEmergencyPhone2"] : <any>null;
            this.healthEmergencyPhone3 = _data["healthEmergencyPhone3"] !== undefined ? _data["healthEmergencyPhone3"] : <any>null;
        }
    }

    static fromJS(data: any): CreateCitizenCommand {
        data = typeof data === 'object' ? data : {};
        let result = new CreateCitizenCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["fatherId"] = this.fatherId !== undefined ? this.fatherId : <any>null;
        data["motherId"] = this.motherId !== undefined ? this.motherId : <any>null;
        data["fullName"] = this.fullName ? this.fullName.toJSON() : <any>null;
        data["address"] = this.address ? this.address.toJSON() : <any>null;
        data["gender"] = this.gender !== undefined ? this.gender : <any>null;
        data["religion"] = this.religion !== undefined ? this.religion : <any>null;
        data["socialStatus"] = this.socialStatus !== undefined ? this.socialStatus : <any>null;
        data["dateOfBirth"] = this.dateOfBirth ? this.dateOfBirth.toISOString() : <any>null;
        data["photo"] = this.photo ? this.photo.toJSON() : <any>null;
        data["bloodType"] = this.bloodType !== undefined ? this.bloodType : <any>null;
        data["healthEmergencyPhone1"] = this.healthEmergencyPhone1 !== undefined ? this.healthEmergencyPhone1 : <any>null;
        data["healthEmergencyPhone2"] = this.healthEmergencyPhone2 !== undefined ? this.healthEmergencyPhone2 : <any>null;
        data["healthEmergencyPhone3"] = this.healthEmergencyPhone3 !== undefined ? this.healthEmergencyPhone3 : <any>null;
        return data;
    }
}

export interface ICreateCitizenCommand {
    fatherId?: string | null;
    motherId?: string | null;
    fullName: FullName;
    address: Address;
    gender?: Gender;
    religion?: Religion;
    socialStatus?: SocialStatus;
    dateOfBirth?: Date;
    photo?: BinaryFile | null;
    bloodType?: BloodType;
    healthEmergencyPhone1?: string | null;
    healthEmergencyPhone2?: string | null;
    healthEmergencyPhone3?: string | null;
}

export class BinaryFile implements IBinaryFile {
    name?: string | null;
    bytes?: string | null;
    length?: number;
    contentType?: string | null;

    constructor(data?: IBinaryFile) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.name = _data["name"] !== undefined ? _data["name"] : <any>null;
            this.bytes = _data["bytes"] !== undefined ? _data["bytes"] : <any>null;
            this.length = _data["length"] !== undefined ? _data["length"] : <any>null;
            this.contentType = _data["contentType"] !== undefined ? _data["contentType"] : <any>null;
        }
    }

    static fromJS(data: any): BinaryFile {
        data = typeof data === 'object' ? data : {};
        let result = new BinaryFile();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["name"] = this.name !== undefined ? this.name : <any>null;
        data["bytes"] = this.bytes !== undefined ? this.bytes : <any>null;
        data["length"] = this.length !== undefined ? this.length : <any>null;
        data["contentType"] = this.contentType !== undefined ? this.contentType : <any>null;
        return data;
    }
}

export interface IBinaryFile {
    name?: string | null;
    bytes?: string | null;
    length?: number;
    contentType?: string | null;
}

/** 0 = None 1 = A 2 = B 3 = O */
export enum BloodType {
    None = 0,
    A = 1,
    B = 2,
    O = 3,
}

export class UpdateCitizenCommand implements IUpdateCitizenCommand {
    id?: string | null;
    fatherId?: string | null;
    motherId?: string | null;
    fullName?: FullName | null;
    address?: Address | null;
    gender?: Gender;
    religion?: Religion;
    socialStatus?: SocialStatus;
    dateOfBirth?: Date;
    photo?: BinaryFile | null;

    constructor(data?: IUpdateCitizenCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"] !== undefined ? _data["id"] : <any>null;
            this.fatherId = _data["fatherId"] !== undefined ? _data["fatherId"] : <any>null;
            this.motherId = _data["motherId"] !== undefined ? _data["motherId"] : <any>null;
            this.fullName = _data["fullName"] ? FullName.fromJS(_data["fullName"]) : <any>null;
            this.address = _data["address"] ? Address.fromJS(_data["address"]) : <any>null;
            this.gender = _data["gender"] !== undefined ? _data["gender"] : <any>null;
            this.religion = _data["religion"] !== undefined ? _data["religion"] : <any>null;
            this.socialStatus = _data["socialStatus"] !== undefined ? _data["socialStatus"] : <any>null;
            this.dateOfBirth = _data["dateOfBirth"] ? new Date(_data["dateOfBirth"].toString()) : <any>null;
            this.photo = _data["photo"] ? BinaryFile.fromJS(_data["photo"]) : <any>null;
        }
    }

    static fromJS(data: any): UpdateCitizenCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateCitizenCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id !== undefined ? this.id : <any>null;
        data["fatherId"] = this.fatherId !== undefined ? this.fatherId : <any>null;
        data["motherId"] = this.motherId !== undefined ? this.motherId : <any>null;
        data["fullName"] = this.fullName ? this.fullName.toJSON() : <any>null;
        data["address"] = this.address ? this.address.toJSON() : <any>null;
        data["gender"] = this.gender !== undefined ? this.gender : <any>null;
        data["religion"] = this.religion !== undefined ? this.religion : <any>null;
        data["socialStatus"] = this.socialStatus !== undefined ? this.socialStatus : <any>null;
        data["dateOfBirth"] = this.dateOfBirth ? this.dateOfBirth.toISOString() : <any>null;
        data["photo"] = this.photo ? this.photo.toJSON() : <any>null;
        return data;
    }
}

export interface IUpdateCitizenCommand {
    id?: string | null;
    fatherId?: string | null;
    motherId?: string | null;
    fullName?: FullName | null;
    address?: Address | null;
    gender?: Gender;
    religion?: Religion;
    socialStatus?: SocialStatus;
    dateOfBirth?: Date;
    photo?: BinaryFile | null;
}

export class EmployeesVm implements IEmployeesVm {
    id?: string | null;
    cardId?: string | null;
    fullName?: FullName | null;
    address?: Address | null;
    gender?: Gender;
    religion?: Religion;
    socialStatus?: SocialStatus;
    dateOfBirth?: Date;
    photoUrl?: string | null;

    constructor(data?: IEmployeesVm) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"] !== undefined ? _data["id"] : <any>null;
            this.cardId = _data["cardId"] !== undefined ? _data["cardId"] : <any>null;
            this.fullName = _data["fullName"] ? FullName.fromJS(_data["fullName"]) : <any>null;
            this.address = _data["address"] ? Address.fromJS(_data["address"]) : <any>null;
            this.gender = _data["gender"] !== undefined ? _data["gender"] : <any>null;
            this.religion = _data["religion"] !== undefined ? _data["religion"] : <any>null;
            this.socialStatus = _data["socialStatus"] !== undefined ? _data["socialStatus"] : <any>null;
            this.dateOfBirth = _data["dateOfBirth"] ? new Date(_data["dateOfBirth"].toString()) : <any>null;
            this.photoUrl = _data["photoUrl"] !== undefined ? _data["photoUrl"] : <any>null;
        }
    }

    static fromJS(data: any): EmployeesVm {
        data = typeof data === 'object' ? data : {};
        let result = new EmployeesVm();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id !== undefined ? this.id : <any>null;
        data["cardId"] = this.cardId !== undefined ? this.cardId : <any>null;
        data["fullName"] = this.fullName ? this.fullName.toJSON() : <any>null;
        data["address"] = this.address ? this.address.toJSON() : <any>null;
        data["gender"] = this.gender !== undefined ? this.gender : <any>null;
        data["religion"] = this.religion !== undefined ? this.religion : <any>null;
        data["socialStatus"] = this.socialStatus !== undefined ? this.socialStatus : <any>null;
        data["dateOfBirth"] = this.dateOfBirth ? this.dateOfBirth.toISOString() : <any>null;
        data["photoUrl"] = this.photoUrl !== undefined ? this.photoUrl : <any>null;
        return data;
    }
}

export interface IEmployeesVm {
    id?: string | null;
    cardId?: string | null;
    fullName?: FullName | null;
    address?: Address | null;
    gender?: Gender;
    religion?: Religion;
    socialStatus?: SocialStatus;
    dateOfBirth?: Date;
    photoUrl?: string | null;
}

export class AddEmployeeCommand implements IAddEmployeeCommand {
    cardId!: string;

    constructor(data?: IAddEmployeeCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.cardId = _data["cardId"] !== undefined ? _data["cardId"] : <any>null;
        }
    }

    static fromJS(data: any): AddEmployeeCommand {
        data = typeof data === 'object' ? data : {};
        let result = new AddEmployeeCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["cardId"] = this.cardId !== undefined ? this.cardId : <any>null;
        return data;
    }
}

export interface IAddEmployeeCommand {
    cardId: string;
}

export class HealthInfoVm implements IHealthInfoVm {
    id?: string | null;
    citizenName?: FullName | null;
    citizenPhoto?: string | null;
    bloodType?: BloodType;
    phone1?: string | null;
    phone2?: string | null;
    phone3?: string | null;
    healthRecords?: HealthRecordVm[] | null;

    constructor(data?: IHealthInfoVm) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data["id"] !== undefined ? _data["id"] : <any>null;
            this.citizenName = _data["citizenName"] ? FullName.fromJS(_data["citizenName"]) : <any>null;
            this.citizenPhoto = _data["citizenPhoto"] !== undefined ? _data["citizenPhoto"] : <any>null;
            this.bloodType = _data["bloodType"] !== undefined ? _data["bloodType"] : <any>null;
            this.phone1 = _data["phone1"] !== undefined ? _data["phone1"] : <any>null;
            this.phone2 = _data["phone2"] !== undefined ? _data["phone2"] : <any>null;
            this.phone3 = _data["phone3"] !== undefined ? _data["phone3"] : <any>null;
            if (Array.isArray(_data["healthRecords"])) {
                this.healthRecords = [] as any;
                for (let item of _data["healthRecords"])
                    this.healthRecords!.push(HealthRecordVm.fromJS(item));
            }
        }
    }

    static fromJS(data: any): HealthInfoVm {
        data = typeof data === 'object' ? data : {};
        let result = new HealthInfoVm();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["id"] = this.id !== undefined ? this.id : <any>null;
        data["citizenName"] = this.citizenName ? this.citizenName.toJSON() : <any>null;
        data["citizenPhoto"] = this.citizenPhoto !== undefined ? this.citizenPhoto : <any>null;
        data["bloodType"] = this.bloodType !== undefined ? this.bloodType : <any>null;
        data["phone1"] = this.phone1 !== undefined ? this.phone1 : <any>null;
        data["phone2"] = this.phone2 !== undefined ? this.phone2 : <any>null;
        data["phone3"] = this.phone3 !== undefined ? this.phone3 : <any>null;
        if (Array.isArray(this.healthRecords)) {
            data["healthRecords"] = [];
            for (let item of this.healthRecords)
                data["healthRecords"].push(item.toJSON());
        }
        return data;
    }
}

export interface IHealthInfoVm {
    id?: string | null;
    citizenName?: FullName | null;
    citizenPhoto?: string | null;
    bloodType?: BloodType;
    phone1?: string | null;
    phone2?: string | null;
    phone3?: string | null;
    healthRecords?: HealthRecordVm[] | null;
}

export class HealthRecordVm implements IHealthRecordVm {
    medications?: string | null;
    diagnosis?: string | null;
    create?: Date;
    createBy?: string | null;
    attachments?: string[] | null;

    constructor(data?: IHealthRecordVm) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.medications = _data["medications"] !== undefined ? _data["medications"] : <any>null;
            this.diagnosis = _data["diagnosis"] !== undefined ? _data["diagnosis"] : <any>null;
            this.create = _data["create"] ? new Date(_data["create"].toString()) : <any>null;
            this.createBy = _data["createBy"] !== undefined ? _data["createBy"] : <any>null;
            if (Array.isArray(_data["attachments"])) {
                this.attachments = [] as any;
                for (let item of _data["attachments"])
                    this.attachments!.push(item);
            }
        }
    }

    static fromJS(data: any): HealthRecordVm {
        data = typeof data === 'object' ? data : {};
        let result = new HealthRecordVm();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["medications"] = this.medications !== undefined ? this.medications : <any>null;
        data["diagnosis"] = this.diagnosis !== undefined ? this.diagnosis : <any>null;
        data["create"] = this.create ? this.create.toISOString() : <any>null;
        data["createBy"] = this.createBy !== undefined ? this.createBy : <any>null;
        if (Array.isArray(this.attachments)) {
            data["attachments"] = [];
            for (let item of this.attachments)
                data["attachments"].push(item);
        }
        return data;
    }
}

export interface IHealthRecordVm {
    medications?: string | null;
    diagnosis?: string | null;
    create?: Date;
    createBy?: string | null;
    attachments?: string[] | null;
}

export class AddHealthRecordCommand implements IAddHealthRecordCommand {
    healthInfoId!: string;
    medications!: string;
    diagnosis!: string;
    attachments?: BinaryFile[] | null;

    constructor(data?: IAddHealthRecordCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.healthInfoId = _data["healthInfoId"] !== undefined ? _data["healthInfoId"] : <any>null;
            this.medications = _data["medications"] !== undefined ? _data["medications"] : <any>null;
            this.diagnosis = _data["diagnosis"] !== undefined ? _data["diagnosis"] : <any>null;
            if (Array.isArray(_data["attachments"])) {
                this.attachments = [] as any;
                for (let item of _data["attachments"])
                    this.attachments!.push(BinaryFile.fromJS(item));
            }
        }
    }

    static fromJS(data: any): AddHealthRecordCommand {
        data = typeof data === 'object' ? data : {};
        let result = new AddHealthRecordCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["healthInfoId"] = this.healthInfoId !== undefined ? this.healthInfoId : <any>null;
        data["medications"] = this.medications !== undefined ? this.medications : <any>null;
        data["diagnosis"] = this.diagnosis !== undefined ? this.diagnosis : <any>null;
        if (Array.isArray(this.attachments)) {
            data["attachments"] = [];
            for (let item of this.attachments)
                data["attachments"].push(item.toJSON());
        }
        return data;
    }
}

export interface IAddHealthRecordCommand {
    healthInfoId: string;
    medications: string;
    diagnosis: string;
    attachments?: BinaryFile[] | null;
}

export class UpdateEmergencyPhonesCommand implements IUpdateEmergencyPhonesCommand {
    healthInfoId?: string | null;
    phone1?: string | null;
    phone2?: string | null;
    phone3?: string | null;

    constructor(data?: IUpdateEmergencyPhonesCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.healthInfoId = _data["healthInfoId"] !== undefined ? _data["healthInfoId"] : <any>null;
            this.phone1 = _data["phone1"] !== undefined ? _data["phone1"] : <any>null;
            this.phone2 = _data["phone2"] !== undefined ? _data["phone2"] : <any>null;
            this.phone3 = _data["phone3"] !== undefined ? _data["phone3"] : <any>null;
        }
    }

    static fromJS(data: any): UpdateEmergencyPhonesCommand {
        data = typeof data === 'object' ? data : {};
        let result = new UpdateEmergencyPhonesCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["healthInfoId"] = this.healthInfoId !== undefined ? this.healthInfoId : <any>null;
        data["phone1"] = this.phone1 !== undefined ? this.phone1 : <any>null;
        data["phone2"] = this.phone2 !== undefined ? this.phone2 : <any>null;
        data["phone3"] = this.phone3 !== undefined ? this.phone3 : <any>null;
        return data;
    }
}

export interface IUpdateEmergencyPhonesCommand {
    healthInfoId?: string | null;
    phone1?: string | null;
    phone2?: string | null;
    phone3?: string | null;
}

export class SignHashCommand implements ISignHashCommand {
    base64Sha512DataHash!: string;

    constructor(data?: ISignHashCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.base64Sha512DataHash = _data["base64Sha512DataHash"] !== undefined ? _data["base64Sha512DataHash"] : <any>null;
        }
    }

    static fromJS(data: any): SignHashCommand {
        data = typeof data === 'object' ? data : {};
        let result = new SignHashCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["base64Sha512DataHash"] = this.base64Sha512DataHash !== undefined ? this.base64Sha512DataHash : <any>null;
        return data;
    }
}

export interface ISignHashCommand {
    base64Sha512DataHash: string;
}

export class VerifySignatureResult implements IVerifySignatureResult {
    valid?: boolean;
    fullName?: FullName | null;
    photo?: string | null;

    constructor(data?: IVerifySignatureResult) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.valid = _data["valid"] !== undefined ? _data["valid"] : <any>null;
            this.fullName = _data["fullName"] ? FullName.fromJS(_data["fullName"]) : <any>null;
            this.photo = _data["photo"] !== undefined ? _data["photo"] : <any>null;
        }
    }

    static fromJS(data: any): VerifySignatureResult {
        data = typeof data === 'object' ? data : {};
        let result = new VerifySignatureResult();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["valid"] = this.valid !== undefined ? this.valid : <any>null;
        data["fullName"] = this.fullName ? this.fullName.toJSON() : <any>null;
        data["photo"] = this.photo !== undefined ? this.photo : <any>null;
        return data;
    }
}

export interface IVerifySignatureResult {
    valid?: boolean;
    fullName?: FullName | null;
    photo?: string | null;
}

export class VerifySignatureCommand implements IVerifySignatureCommand {
    base64Sha512DataHash!: string;
    signature!: string;

    constructor(data?: IVerifySignatureCommand) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.base64Sha512DataHash = _data["base64Sha512DataHash"] !== undefined ? _data["base64Sha512DataHash"] : <any>null;
            this.signature = _data["signature"] !== undefined ? _data["signature"] : <any>null;
        }
    }

    static fromJS(data: any): VerifySignatureCommand {
        data = typeof data === 'object' ? data : {};
        let result = new VerifySignatureCommand();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["base64Sha512DataHash"] = this.base64Sha512DataHash !== undefined ? this.base64Sha512DataHash : <any>null;
        data["signature"] = this.signature !== undefined ? this.signature : <any>null;
        return data;
    }
}

export interface IVerifySignatureCommand {
    base64Sha512DataHash: string;
    signature: string;
}

export class ApiException extends Error {
    message: string;
    status: number;
    response: string;
    headers: { [key: string]: any; };
    result: any;

    constructor(message: string, status: number, response: string, headers: { [key: string]: any; }, result: any) {
        super();

        this.message = message;
        this.status = status;
        this.response = response;
        this.headers = headers;
        this.result = result;
    }

    protected isApiException = true;

    static isApiException(obj: any): obj is ApiException {
        return obj.isApiException === true;
    }
}

function throwException(message: string, status: number, response: string, headers: { [key: string]: any; }, result?: any): Observable<any> {
    if (result !== null && result !== undefined)
        return _observableThrow(result);
    else
        return _observableThrow(new ApiException(message, status, response, headers, null));
}

function blobToText(blob: any): Observable<string> {
    return new Observable<string>((observer: any) => {
        if (!blob) {
            observer.next("");
            observer.complete();
        } else {
            let reader = new FileReader();
            reader.onload = event => {
                observer.next((<any>event.target).result);
                observer.complete();
            };
            reader.readAsText(blob);
        }
    });
}
