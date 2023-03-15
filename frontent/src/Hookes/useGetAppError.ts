import { SerializedError } from "@reduxjs/toolkit"
import { FetchBaseQueryError } from "@reduxjs/toolkit/dist/query"
import AppError from "../Models/Global/AppError"

const useGetAppError = (error: FetchBaseQueryError | SerializedError | undefined) => {
    if (error == undefined)
        return error

    return (error as FetchBaseQueryError).data as AppError
}

export default useGetAppError