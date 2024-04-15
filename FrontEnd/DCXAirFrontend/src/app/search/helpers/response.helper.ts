import { HttpErrorResponse, HttpResponse } from "@angular/common/http";
import { CommonResponse } from "../models/Response";

export class ResponseHelper {
  static responseDontHaveErrors(response: CommonResponse<any>) {
    return response.data &&
      !('error' in response.data);
  }

  static generateCommonResponse(response: HttpResponse<any>) {
    let commonResponse = new CommonResponse();

    commonResponse.data = response.body?.data || undefined;
    commonResponse.message = response.body.message;

    return commonResponse;
  }

  static generateCommonResponseFromHttpErrorResponse(response: HttpErrorResponse) {
    let commonResponse = new CommonResponse();

    commonResponse.message = response.error.message;
    return commonResponse;
  }
}
