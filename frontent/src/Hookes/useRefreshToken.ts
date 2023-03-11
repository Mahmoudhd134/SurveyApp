import { BASE_URL } from "../App/Api/baseApi"

const useRefreshToken = () => {
    const refresh = async () => {
        const refreshToken = await fetch(`${BASE_URL}auth/RefreshToken`, { method: 'GET', credentials: 'include' })
        return await refreshToken.json()
    }
    return refresh
}

export default useRefreshToken