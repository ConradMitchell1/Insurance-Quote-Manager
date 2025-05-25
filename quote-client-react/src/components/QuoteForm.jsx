import { useState} from 'react';
import { PolicyTypes, QuoteStatuses } from '../utils/enums';
function QuoteForm({ initialData, onSubmit, onClose, onSuccess, mode = 'create' }) {
    const [form, setForm] = useState({
        clientName: '',
        email: '',
        clientAge: '',
        policyType: 0,
        quoteStatus: 0,
        propertyType: '',
        constructionType: '',
        propertyAge: 0,
        expiryDate: new Date().toISOString().split('T')[0],
        isSmoker: false,
        hasChronicIllness: false,
        ...initialData,
    });
    const [errors, setErrors] = useState([]);

    const handleChange = (field, value) => {
        setForm(prev => ({ ...prev, [field]: value }));
    };

    const handleSubmit = async () => {
        try {
            const age = parseInt(form.clientAge, 10);
            const validationErrors = [];
            if (isNaN(age)) {
                form.clientAge = 10;
                validationErrors.push("Client age is required and must be a positive number.");
            }
            const payload = {
                ...form,
                clientAge: age,
                isSmoker: !!form.isSmoker,
                hasChronicIllness: !!form.hasChronicIllness
            };
            console.log("Submitting payload:", payload);
            const response = await onSubmit(payload);
            if (response.ok) {
                setErrors([]);
                if (typeof onSuccess === 'function') {
                    onSuccess();
                }
                onClose();
            } else {
                const errorJson = await response.json()
                for (const key in errorJson.errors || {}) {
                    validationErrors.push(...errorJson.errors[key]);
                }
                form.clientAge = age;
                setErrors(validationErrors.length ? validationErrors : [errorJson.title]);
            }
        } catch (err) {
            setErrors([err.message || 'Network error']);
        }
    };
    return (
        <div className="dialog-overlay">
            <div className="dialog">
                <h2>{mode === 'edit' ? 'Update Quote' : 'Create New Quote'}</h2>
                {errors.length > 0 && (
                    <div style={{ color: 'red', marginBottom: '1rem' }}>
                        <ul>
                            {errors.map((err, i) => (
                                <li key={i}>{err}</li>
                            ))}
                        </ul>
                    </div>
                )}
                <input placeholder="Client Name" value={form.clientName} onChange={(e) => handleChange('clientName', e.target.value)} />
                <input placeholder="Email" value={form.email} onChange={(e) => handleChange('email', e.target.value)} />
                <input type="number" placeholder="Age" value={form.clientAge} onChange={(e) => handleChange('clientAge', e.target.value)} />
                <select value={form.policyType} onChange={(e) => handleChange('policyType', parseInt(e.target.value))}>
                    {Object.entries(PolicyTypes).map(([value, label]) => (
                        <option key={value} value={value}>{label}</option>
                    ))}
                </select>

                <select
                    value={form.quoteStatus}
                    onChange={(e) => handleChange('quoteStatus', parseInt(e.target.value))}
                >
                    {Object.entries(QuoteStatuses).map(([value, label]) => (
                        <option key={value} value={value}>{label}</option>
                    ))}
                </select>
                {mode === 'create' && (
                    <>
                    < input type="date" value={form.expiryDate} onChange={(e) => handleChange('expiryDate', e.target.value)} />
                    </>
                ) }
                
                {form.policyType === 0 && (
                    <>
                        <input type="checkbox" checked={form.isSmoker} onChange={(e) => handleChange('isSmoker', e.target.checked)} />
                        <label>Is Smoker</label>
                        <input type="checkbox" checked={form.hasChronicIllness} onChange={(e) => handleChange('hasChronicIllness', e.target.checked)} />
                        <label>Has Chronic Illness</label>
                    </>
                )}

                {form.policyType === 2 && (
                    <>
                        <input placeholder="Property Type" value={form.propertyType} onChange={(e) => handleChange('propertyType', e.target.value)} />
                        <input placeholder="Construction Type" value={form.constructionType} onChange={(e) => handleChange('constructionType', e.target.value)} />
                        <input type="number" placeholder="Property Age" value={form.propertyAge} onChange={(e) => handleChange('propertyAge', e.target.value)} />
                    </>
                )}
                <button onClick={handleSubmit}>{mode === 'edit' ? 'Update' : 'Submit'}</button>
                <button onClick={onClose} >Cancel</button>
            </div>

        </div>
    );
    
}
export default QuoteForm;