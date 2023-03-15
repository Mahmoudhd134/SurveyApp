import { useGetStatusQuery } from "../../Feutures/Auth/authApi";

const Profile = () => {
    const result = useGetStatusQuery()

    return (
        <section>
            <h1>{JSON.stringify(result, null, '\n')}</h1>
        </section>
    );
};

export default Profile;