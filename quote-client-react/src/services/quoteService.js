const BASE_URL = 'https://localhost:7152/api/Quote';

export async function getQuotes(searchQuotes) {
    const url = new URL(BASE_URL);
    if (searchQuotes) {
        url.searchParams.append('searchTerm', searchQuotes);
    }

    const response = await fetch(url);
    return response.json();
}

export async function createQuote(quote) {
    const response = await fetch(BASE_URL, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(quote)
    });
    return response;
}

export async function updateQuote(quote) {
    const response = await fetch(BASE_URL, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(quote)
    });
    return response;
}

export async function deleteQuote(id) {
    const response = await fetch(`${BASE_URL}/${id}`, {
        method: 'DELETE',
    });
    return response;
}