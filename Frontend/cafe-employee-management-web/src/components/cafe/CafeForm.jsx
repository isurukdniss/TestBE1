
import { useForm } from "react-hook-form";
import TextBox from "../common/TextBox";
import { useEffect, useState } from "react";
import { getCafe, createCafe, updateCafe } from "../../api";
import { useParams, useNavigate, useBeforeUnload } from "react-router-dom";
import { ToastContainer, toast } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css';

export default function CafeForm() {
    const {
        register,
        handleSubmit,
        reset,
        formState: { errors, isDirty },
    } = useForm()

    const [loading, setLoading] = useState(true);
    const { id } = useParams();
    const navigate = useNavigate();

    const loadCafeById = async () => {
        try {
            const response = await getCafe(id);
            const formData = response.data.data;
            reset({
                name: formData.name || '',
                description: formData.description || '',
                logo: formData.logo || '',
                location: formData.location || '',
            })

        } catch (error) {
            console.log('Error loading employee data: ', error);
        }
    }

    useEffect(() => {
        if (id) {
            loadCafeById(id);
        }
    }, []);

    const onSubmit = async (data) => {
        try {
            if (id) {
                // PUT
                data.id = id;
                const response = await updateCafe(id, data);
                if (response && response.data && response.data.success) {
                    toast.success('Successfully updated the employee.');
                    console.log('Cafe updated');
                    setTimeout(() => {
                        navigate('/cafes');
                    }, 1000);

                    reset();
                }
            } else {
                // POST
                const response = await createCafe(data);
                if (response && response.data && response.data.success) {
                    toast.success('Successfully created the cafe.');
                    console.log(`Cafe created. Id = ${response.data.data}`);
                    navigate('/cafes');

                    reset();
                }
            }
        } catch (err) {
            console.error('Error creating cafe: ' + err);
            toast.error('Error creating cafe. Please try again.');
        }
    };

    const handleCancel = () => {
        console.log(isDirty);
        if (!isDirty || window.confirm('You have unsaved changes. Do you really want to leave?')) {
            navigate('/cafes');
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
                    label="Description"
                    fieldName="description"
                    register={register}
                    isRequired={true}
                    errors={errors}
                    maxLength={256}
                />
                <div>
                    <label htmlFor="logo">Upload Logo</label>
                    <input
                        type="file"
                        id="logo"
                        {...register('logo', {
                            validate: {
                                lessThan2MB: (files) =>
                                    !files[0] || files[0]?.size < 2 * 1024 * 1024 || 'File size should be less than 2MB',
                                acceptedFormats: (files) =>
                                    !files[0] ||
                                    ['image/jpeg', 'image/png'].includes(files[0]?.type) ||
                                    'Only JPEG or PNG files are allowed',
                            },
                        })}
                    />
                    {errors.logo && <p style={{ color: 'red' }}>{errors.logo.message}</p>}
                </div>
                <TextBox
                    label="Location"
                    fieldName="location"
                    register={register}
                    isRequired={true}
                    errors={errors}
                    maxLength={50}
                />
                <button type="submit">Submit</button>
                <button type="button" onClick={handleCancel}>Cancel</button>
            </form>
            <ToastContainer />
        </>
    );
}