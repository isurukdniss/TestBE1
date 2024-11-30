import { React } from "react";

const TextBox = ({
    label, 
    fieldName, 
    register, 
    errors, 
    placeHolder, 
    isRequired, 
    maxLength, 
    minLength,
    type }) => {
        return (
            <div className="form-field"> 
                <label htmlFor={fieldName} style={{ display: 'block', marginBottom: '5px' }}>
                    {label}
                </label>
                {/* <input placeholder= {placeHolder} {...register(fieldName, {
                    required: {
                    value: isRequired,
                    message: "This is required"
                    }, 
                    maxLength: {
                    value: maxLength,
                    message: `Value must be maximum ${maxLength}`
                    }, 
                    minLength: {
                    value: minLength,
                    message: `Value must be minimum ${minLength}`
                    },
                    })}
                /> */}
                <input
                    id={fieldName}
                    name={fieldName}
                    placeholder={placeHolder} 
                    type={type || 'text'} 
                    {...register(fieldName, {
                    required: isRequired ? `${label} is required` : false,
                    minLength: minLength
                        ? {
                            value: minLength,
                            message: `${label} must be at least ${minLength} characters`,
                        }
                        : undefined,
                    maxLength: maxLength
                        ? {
                            value: maxLength,
                            message: `${label} cannot exceed ${maxLength} characters`,
                        }
                        : undefined,
                    pattern: type === 'email' ? {
                        value: /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/,
                        message: 'Please enter a valid email address'
                    } : 
                    type === 'phone' 
                        ? {
                            value: /^[89][0-9]{7}$/,
                            message: 'Phone number must start with 8 or 9 and have exactly 8 digits',
                        }
                    :  undefined,
                    })} 
                /> 

                {errors[fieldName] && (
                    <span style={{ color: 'red', fontSize: '12px' }}>{errors[fieldName].message}</span>
                )}
            </div> 
        );
};

export default TextBox;

