import { useEffect, useState } from 'react';
import CreateQuote from './components/CreateQuote';
import QuotesTable from './components/QuotesTable';
import EditQuoteDialog from './components/EditQuoteDialog';
import './App.css'
import { getQuotes } from './services/quoteService';
function App() {
    const [quotes, setQuotes] = useState([]);
    const [editingQuote, setEditingQuote] = useState(null);
    const [searchQuote, setSearchQuote] = useState('');
    const [showCreateDialog, setShowCreateDialog] = useState(false);

    const fetchQuotes = () => {
        getQuotes(searchQuote).then(setQuotes).catch(console.error);
    }

    useEffect(() => {
        fetchQuotes();
    },[]);

    return (
        <div className="page-container">
            <h1>Quotes</h1>

            <button onClick={() => setShowCreateDialog(true)}>Create New Quote</button>
            <input placeholder="Search Table.." value={searchQuote} onChange={(e) => setSearchQuote(e.target.value) }></input>
            <button onClick={fetchQuotes}>search</button>
            <QuotesTable quotes={quotes} onDelete={fetchQuotes} onEdit={setEditingQuote} />

            {showCreateDialog && (
                <CreateQuote onCreated={() => { fetchQuotes(); setShowCreateDialog(false); }} onClose={() => setShowCreateDialog(false) } />
            )}


            {editingQuote && (
                <EditQuoteDialog quote={editingQuote} onClose={() => setEditingQuote(null)} onUpdated={fetchQuotes}/>
            )}
        </div>
    );
}

export default App;