import { useState } from 'react';
import { createQuote } from '../services/quoteService';
import { PolicyTypes, QuoteStatuses } from '../utils/enums';

function CreateQuote({ onCreated }) {
    const [form, setForm] = useState({
        clientName: '',
        email: '',
        clientAge: 0,
        policyType: 0,
        quoteStatus: 0,
        propertyType: '',
        constructionType: '',
        propertyAge: 0,
        expiryDate: new Date().toISOString().split('T')[0]
    });

    const [errors, setErrors] = useState([]);

    const handleChange = (field, value) => {
        setForm(prev => ({ ...prev, [field]: value }));
    };

    const handleSubmit = async () => {
        try {
            const response = await createQuote(form);
            if (response.ok) {
                onCreated();
                setErrors([]);
            } else {
                const errorJson = await response.json();
                const validationErrors = [];
                for (const key in errorJson.errors || {}) {
                    validationErrors.push(...errorJson.errors[key]);
                }
                setErrors(validationErrors.length ? validationErrors : [errorJson.title]);
            }
        } catch (err) {
            setErrors([err.message || 'Network error']);
        }
    };

    return (
        <div>
            <h2>Create New Quote</h2>
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

            <input type="date" value={form.expiryDate} onChange={(e) => handleChange('expiryDate', e.target.value)}/>

            {form.policyType === 2 && (
                <>
                    <input placeholder="Property Type" value={form.propertyType} onChange={(e) => handleChange('propertyType', e.target.value)}/>
                    <input placeholder="Construction Type" value={form.constructionType} onChange={(e) => handleChange('constructionType', e.target.value)}/>
                    <input type="number" placeholder="Property Age" value={form.propertyAge} onChange={(e) => handleChange('propertyAge', e.target.value)} />
                </>
            )}
            <button onClick={handleSubmit}>Submit</button>
        </div>
    );
}

export default CreateQuote;