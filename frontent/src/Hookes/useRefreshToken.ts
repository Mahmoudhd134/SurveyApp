import { BASE_URL } from "../App/Api/baseApi"
import TokenModel from "../Models/Auth/TokenModel"

const useRefreshToken = () => {
    const refresh = async () => {
        const refreshToken = await fetch(`${BASE_URL}auth/RefreshToken`,
            {
                method: 'GET',
                credentials: 'include'
            })
        return await refreshToken.json() as TokenModel
    }
    return refresh
}

export default useRefreshToken