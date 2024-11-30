
import { useForm } from "react-hook-form";
import TextBox from "../common/TextBox";
import { useEffect, useState } from "react";
import { getCafes, getEmployee, createEmployee, updateEmployee } from "../../api";
import { useParams, useNavigate, useBeforeUnload } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

export default function EmployeeForm() {
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors, isDirty },
    } = useForm()

    const [cafes, setCafes] = useState([]);
    const [loading, setLoading] = useState(true);
    const { id } = useParams();
    const navigate = useNavigate();

    useBeforeUnload((event) => {
        if (isDirty) {
            event.preventDefault();
        }
    });

    const loadCafes = async () => {
        try {
            const response = await getCafes();
            const { success, data, errors } = response.data;

            if (success) {
                setCafes(data);
            } else {
                setError(errors.length > 0 ? errors.join(', ') : "Unknown error occurred");
            }
        } catch (err) {
            setError(err.message || 'Failed to fetch data');
        } finally {
            setLoading(false);
        }
    }

    const loadEmployeeById = async () => {
        try {
            const response = await getEmployee(id);
            const formData = response.data.data;
            reset({
                name: formData.name || '',
                email: formData.email || '',
                phoneNumber: formData.phoneNumber || '',
                gender: formData.gender.toString() || '',
                cafeId: formData.cafeId || '',
            })

        } catch (error) {
            console.log('Error loading employee data: ', error);
        }
    }

    useEffect(() => {
        if (id) {
            loadEmployeeById(id);
        }

        loadCafes();
    }, []);

    const onSubmit = async (data) => {
        const formattedData = {
            ...data,
            gender: Number(data.gender),
        };
        try {
            if (id) {
                // PUT
                formattedData.id = id;
                const response = await updateEmployee(id, formattedData);
                if (response && response.data && response.data.success) {
                    toast.success('Successfully updated the employee.');
                    console.log('Employee updated');
                    setTimeout(() => {
                        navigate('/employees');
                    }, 1000);

                    reset();
                }
            } else {
                // POST
                const response = await createEmployee(formattedData);
                if (response && response.data && response.data.success) {
                    toast.success('Successfully created the employee.');
                    console.log(`Employee created. Id = ${response.data.data}`);
                    navigate('/employees');

                    reset();
                }
            }
        } catch (err) {
            console.error('Error creating employee: ' + err);
            toast.error('Error creating employee. Please try again.');
        }
    };

    const handleCancel = () => {
        console.log(isDirty);
        if (!isDirty || window.confirm('You have unsaved changes. Do you really want to leave?')) {
            navigate('/employees');
        }
    }

    return (
        <>
            <form onSubmit={handleSubmit(onSubmit)}>
                <TextBox
                    label="Name"
                    fieldName="name"
                    register={register}
                    isRequired={true}
                    errors={errors}
                    minLength={6}
                    maxLength={10}
                />
                <TextBox
                    label="Email"
                    fieldName="email"
                    register={register}
                    isRequired={true}
                    errors={errors}
                    type="email"
                />
                <TextBox
                    label="Phone Number"
                    fieldName="phoneNumber"
                    register={register}
                    errors={errors}
                    isRequired={true}
                    type="phone"
                />
                <div>
                    <label>Gender</label>
                    <div>
                        <input type="radio" name="male" value="1"  {...register('gender')} />
                        <label htmlFor="male">Male</label>
                    </div>
                    <div>
                        <input type="radio" name="female" value="2" {...register('gender')} />
                        <label htmlFor="female">Female</label>
                    </div>
                    {errors.gender && (
                        <span style={{ color: 'red', fontSize: '12px' }}>{errors.gender.message}</span>
                    )}
                </div>
                <div>
                    <label htmlFor="cafe">Assigned Cafe</label>
                    {loading ? (
                        <p>Loading Cafes...</p>
                    ) : (
                        <select {...register('cafeId', { required: 'Please select a cafe' })}>
                            <option selected="selected" value="" disabled>-- Select a Cafe --</option>
                            {cafes.map((cafe) => (
                                <option key={cafe.id} value={cafe.id}>{cafe.name}</option>
                            ))}
                        </select>
                    )}
                    {errors.cafe && (
                        <span style={{ color: 'red', fontSize: '12px' }}>{errors.cafe.message}</span>
                    )}
                </div>

                <button type="submit">Submit</button>
                <button type="button" onClick={handleCancel}>Cancel</button>
            </form>
            <ToastContainer />
        </>
    );
}