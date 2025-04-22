import User from "../components/User";
import { useAuth } from "../contexts/AuthContext";

const Register = () => {

  const { register } = useAuth(); 

  return (
    <User func={register}>
    </User>
  );
}

export default Register;