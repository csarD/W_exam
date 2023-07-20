

import { Formik, Field, Form } from 'formik';
import axios from "axios";
import {Navigate} from "react-router-dom";
import {useState} from "react";

function App() {
    const [user,setUser]=useState(false)
    return (
        <>
            {user &&
                <Navigate to="/HomePage" replace={true} />}

            <Formik
                initialValues={{

                    data: ''

                }}
                onSubmit={async (values) => {


                    try {
                       /* const data= await axios.post("http://localhost/api/login/authenticate",values,{
                            headers: { 'Content-Type': 'application/json'}
                        })
                        // eslint-disable-next-line @typescript-eslint/no-unsafe-member-access,@typescript-eslint/restrict-template-expressions
                        axios.defaults.headers.common['Authorization'] = `Bearer ${data?.data?.token}`;
                        setUser(true)*/
                        const data= await axios.post("http://localhost/api/customer",values)
                        console.log(data)
                    }catch (e) {
                        alert(e.response.data.message)
                    }



                }}
            >
                {({ isSubmitting }) => (
                    <Form>

                        <label htmlFor="lastName">Data</label>
                        <Field type="text" name="data" placeholder="[0,14,2,64,8,79,6,5,123]" />

                        <button type="submit" disabled={isSubmitting}>
                            Submit
                        </button>
                    </Form>
                )}
            </Formik>
        </>
    );

}

export default App
